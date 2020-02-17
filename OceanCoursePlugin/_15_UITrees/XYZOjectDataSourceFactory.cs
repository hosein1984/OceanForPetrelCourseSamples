using System;
using Slb.Ocean.Core;
using Slb.Ocean.Data.Hosting;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.Data.Persistence;

namespace OceanCoursePlugin._15_UITrees
{
    /// 
    /// [Monday, February 17, 2020] Generated by Schlumberger Ocean Data Source Wizard
    /// 
    /// Author   : Hosein
    /// 
    /// <summary>
    /// Factory for custom data sources. 
    /// For each Petrel project, Petrel will ask the registered factories to return the (non serializable) custom data source. 
    /// A data source returned should not be added to the data source manager by the plug-in (it will be added automatically). 
    /// </summary>
    public class XYZOjectDataSourceFactory : DataSourceFactory
    {
        private static XYZOjectDataSourceFactory _instance;
        private static string DataSourceId = "{2986B8C3-AD56-4E15-8D89-67F40081F304}";

        private XYZOjectDataSourceFactory()
        {
        }

        public static XYZOjectDataSourceFactory Instance => _instance ?? (_instance = new XYZOjectDataSourceFactory());

        /// <summary>
        /// Gets the data source with corresponding source Id.
        /// </summary>
        public static StructuredArchiveDataSource Get(IDataSourceManager dataSourceManager)
        {
            return dataSourceManager.GetSource(DataSourceId) as StructuredArchiveDataSource;
        }

        /// <summary>
        /// Gets the data source that uses StructuredArchive serialization. 
        /// Data stored in StructuredArchiveDataSource is only saved if IsDirty is true. 
        /// The flag is automatically set to true if a new object is added to the datasource, but plug-ins are responsible to set IsDirty on any change in the domain object data which must be serialized. 
        /// </summary>
        public override IDataSource GetDataSource()
        {
            //Add custom domain object types into this array
            Type[] supportedTypes = {typeof(XYZObject)};
            StructuredArchiveDataSource dataSource = new StructuredArchiveDataSource(DataSourceId, supportedTypes);
            //Register archivable surrogates here
            //dataSource.AddArchivableSurrogate(new XYZOjectDataSourceFactoryFacadeSurrogate<Borehole>());
            return dataSource;
        }
    }

    /// <summary>
    /// Surrogate for native domain objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public class XYZOjectDataSourceFactoryFacadeSurrogate<T> : ArchivableSurrogate<T> where T : class, IIdentifiable, IDomainObject
    {
        protected override T Serialize(IStructuredArchive ar, T value)
        {
            ar.SerializeReference("Droid", () => value.Droid, (droid) => CoreSystem.GetService<IDomainObjectHost>().Admin.ToIdentifiableFacade<T>(droid));
            return value;
        }
    }
}