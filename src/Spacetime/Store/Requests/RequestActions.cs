using Fluxor;
using Spacetime.Core.Infrastructure;
using Spacetime.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Store.Requests
{
    public class FetchRequestsAction
    {
    }

    public class FetchRequestsSuccessAction
    {
        public FetchRequestsSuccessAction(List<SpacetimeRequest> requests)
        {
            Requests = requests;
        }

        public List<SpacetimeRequest> Requests { get; set; }
    }

    public class FilterRequestsAction
    {
        public FilterRequestsAction(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; set; }
    }

    public class ShowRequestAction
    {
        public ShowRequestAction(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class DeleteRequestAction
    {
        public DeleteRequestAction(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class DeleteRequestSuccessAction
    {
        public DeleteRequestSuccessAction(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class AddRequestAction
    {
        public AddRequestAction(SpacetimeRequest request)
        {
            Request = request;
        }

        public SpacetimeRequest Request { get; private set; }
    }
    public class AddRequestSuccessAction
    {
        public AddRequestSuccessAction(SpacetimeRequest request)
        {
            Request = request;
        }

        public SpacetimeRequest Request { get; private set; }
    }
}
