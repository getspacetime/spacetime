<div align="center">
[![Build Status](https://github.com/getspacetime/spacetime/actions/workflows/actions.yml/badge.svg)]

<h3 align="center">spacetime</h3>

  <p align="center">
    A fully featured cross-platform, cross-transport API Client, wormholes excluded.
    <br />
    <a href="https://github.com/spacetimedotnet/spacetime"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/spacetimedotnet/spacetime/issues/new?assignees=&labels=&template=bug_report.md&title=">Report Bug</a>
    ·
    <a href="https://github.com/spacetimedotnet/spacetime/issues/new?assignees=&labels=&template=feature_request.md&title=">Request Feature</a>
  </p>
</div>
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## About The Project
A fully featured* cross-platform, cross-transport API Client, wormholes excluded. 

Spacetime is proudly built with .NET MAUI Blazor.

*not quite fully featured yet*


<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

* [.NET MAUI Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-6.0)
* [LiteDB](https://www.litedb.org/)
* [TailwindCSS](https://tailwindcss.com/)
* [Flurl](https://flurl.dev/)
* [Webpack](https://github.com/webpack/webpack)
* [npm](https://www.npmjs.com/)
<p align="right">(<a href="#top">back to top</a>)</p>

## Getting started

### Prerequisites
* [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview/)
* [.NET MAUI Workload](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation) - currently RC1

<p align="right">(<a href="#top">back to top</a>)</p>

### Running locally

There are two steps to running locally. First, the front end resources (JavaScript and Tailwind) need to be bundled using Webpack.

1. Install NPM
2. Install Webpack
3. In the `wwwroot` folder, run the command `npm run dev`

Once you have run the above npm command, you can start debugging in Visual Studio. When you make changes, be sure to restart the application (hot reloading doesn't seem to work quite right yet).

### Installation

#### Windows

Use the [MSIX Installer](https://github.com/spacetimedotnet/spacetime/releases)

#### Mac
Clone the repository and run the `dotnet` command in the Spacetime project folder. **Note**: don't forget to run `npm run build` inside the `wwwroot` folder.

```
dotnet build -t:Run -f net6.0-maccatalyst
```

**Installer*
[TBD](https://github.com/spacetimedotnet/spacetime/issues/5)

#### iOS
[TBD](https://github.com/spacetimedotnet/spacetime/issues/6)

#### Android
[TBD](https://github.com/spacetimedotnet/spacetime/issues/7)

<p align="right">(<a href="#top">back to top</a>)</p>

## Roadmap
- [ ] UX
    - [ ] Responsive design
    - [ ] Keyboard shortcuts
- [ ] Core
    - [ ] User settings
    - [ ] Security settings
    - [ ] Request history
    - [ ] Data Freedom (Exports)
- [ ] Platform
    - [x] Windows
    - [ ] Mac _(in progress)_
    - [ ] Linux
    - [ ] iOS
    - [ ] Android
- [ ] REST
    - [ ] Full support for basic REST operations _(in progress)_
- [ ] gRPC
    - [ ] GRPC Explorer _(in progress)_
    - [ ] Unary support _(in progress)_
    - [ ] Streaming support
- [ ] Kafka
    - [ ] Produce messages
    - [ ] Consume messages
- [ ] Websockets

<p align="right">(<a href="#top">back to top</a>)</p>

## Contributing
Open an issue or tweet me on Twitter with any suggestions or bug reports.

<p align="right">(<a href="#top">back to top</a>)</p>

## Contact
Cody Mullins - [@codemullins](https://twitter.com/codemullins)
<p align="right">(<a href="#top">back to top</a>)</p>
