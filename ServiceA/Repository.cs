using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceA
{
    internal interface IRepository
    {
        void Warmup();
    }
    internal class Repository : IRepository,IDisposable
    {
        private readonly Dictionary<uint, VectorStream> _nodeIdToStreamEntryCache = new();

        public void Dispose()
        {
            
        }
        ~Repository()
        {

        }

        public void Warmup()
        {

            for (uint i = 0; i < 100000; i++)
                _nodeIdToStreamEntryCache.Add(i, new VectorStream
                {
                    DataRate = Duration.FromSeconds(1000),
                    NodeReferences = new List<NodeReference>(1000) { { new NodeReference { NodeId = i, Path = i.ToString(), DisplayPath = i.ToString() } } },
                    SampleType = SampleType.Interval,
                });
        }
    }
}
