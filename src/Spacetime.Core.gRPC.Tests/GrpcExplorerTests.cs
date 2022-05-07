using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using DynamicGrpc;
using FluentAssertions;
using Grpc.Core;
using Moq;
using Spacetime.Core.gRPC.Dynamic;
using Xunit;

namespace Spacetime.Core.gRPC.Tests
{
    public class GrpcExplorerTests
    {
        private const string DefaultUrl = "http://localhost:9001";
        private const string DefaultMethod = "SayHello";
        private const string DefaultService = "greet.Greeter";
        private const string DefaultJson = "{\"foo\":\"bar\"}";

        [Theory]
        [AutoDomainData]
        public async Task Invoke_HappyPath_ShouldSucceed(
            [Frozen] Mock<IDynamicGrpcWrapper> client, 
            [Frozen]Mock<IDynamicGrpcFactory> factory, 
            GrpcExplorer sut)
        {
            IDictionary<string, object> clientResponse = new Dictionary<string, object>();
            clientResponse.Add("foo", "bar");

            SetupFactory(client, factory);
            SetupClient(client, clientResponse);

            var expected = new GrpcResponse { Status = GrpcStatus.Ok, ResponseBody = DefaultJson};

            var actual = await sut.Invoke(DefaultUrl, DefaultService, DefaultMethod, DefaultJson);
            actual.Status.Should().Be(expected.Status);
            actual.ResponseBody.Should().Be(expected.ResponseBody);
        }

        [Theory]
        [AutoDomainData]
        public async Task Invoke_AnyException_ShouldReturnFailure(
            [Frozen] Mock<IDynamicGrpcWrapper> client, 
            [Frozen]Mock<IDynamicGrpcFactory> factory, 
            GrpcExplorer sut)
        {
            SetupFactory(client, factory);
            SetupClientThrow(client);

            var expected = new GrpcResponse { Status = GrpcStatus.Error, ResponseBody = "oops" };

            var actual = await sut.Invoke(DefaultUrl, DefaultService, DefaultMethod, DefaultJson);
            actual.Status.Should().Be(expected.Status);
            actual.ResponseBody.Should().Be(expected.ResponseBody);
        }

        private void SetupClientThrow(Mock<IDynamicGrpcWrapper> client)
        {
            client.Setup(p => p.AsyncUnaryCall(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>())).ThrowsAsync(new Exception("oops"));
        }

        private void SetupClient(Mock<IDynamicGrpcWrapper> client, IDictionary<string, object> returns)
        {
            client.Setup(p => p.AsyncUnaryCall(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(returns);
        }

        private void SetupFactory(Mock<IDynamicGrpcWrapper> client, Mock<IDynamicGrpcFactory> factory)
        {
            factory.Setup(p => p.FromServerReflection(It.IsAny<ChannelBase>(), It.IsAny<DynamicGrpcClientOptions>(),
                    It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(client.Object);
        }
    }

    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}