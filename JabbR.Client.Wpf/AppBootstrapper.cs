﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
//using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;
using JabbR.Client.Wpf.Shell;

namespace JabbR.Client.Wpf {
    public class AppBootstrapper : Bootstrapper<IShell> {
        private CompositionContainer _container;

        /// <summary>
        /// By default, we are configured to use MEF
        /// </summary>
        protected override void Configure() {
            var catalog = new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x))//.OfType<ComposablePartCatalog>()
                );

            _container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            batch.AddExportedValue(catalog);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key) {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);
            var firstExport = exports.FirstOrDefault();
            if (firstExport != null) {
                return firstExport;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance) {
            _container.SatisfyImportsOnce(instance);
        }
    }
}