using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private Dictionary<string, Package> packagesById = new Dictionary<string, Package>();
        private Dictionary<string, Deliverer> deliverersById = new Dictionary<string, Deliverer>();

        public void AddDeliverer(Deliverer deliverer)
        {
            deliverersById.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {
            packagesById.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!deliverersById.ContainsKey(deliverer.Id))
            {
                throw new ArgumentException();
            }
            if (!packagesById.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }

            deliverer.Packages.Add(package);
            package.Deliverer = deliverer;
        }

        public bool Contains(Deliverer deliverer)
            => deliverersById.ContainsKey(deliverer.Id);

        public bool Contains(Package package)
            => packagesById.ContainsKey(package.Id);

        public IEnumerable<Deliverer> GetDeliverers()
            => deliverersById.Values;

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
            => deliverersById.Values.OrderByDescending(d => d.Packages.Count).ThenBy(d => d.Name);

        public IEnumerable<Package> GetPackages()
            => packagesById.Values;

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
            => packagesById.Values.OrderByDescending(p => p.Weight).ThenBy(p => p.Receiver);

        public IEnumerable<Package> GetUnassignedPackages()
            => packagesById.Values.Where(p => p.Deliverer is null);
    }
}
