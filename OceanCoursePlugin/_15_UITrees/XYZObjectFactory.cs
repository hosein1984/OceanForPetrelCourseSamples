using Slb.Ocean.Petrel.UI;

namespace  OceanCoursePlugin._15_UITrees
{
	/// <summary>
	/// PresentationFactory can retreive the appropriate Presentation instance
	/// for the given domainobject type.
	/// Singleton class.
	/// </summary>
	class XYZObjectFactory : INameInfoFactory, IImageInfoFactory
	{
	    /// <summary>
		/// Gets the singleton instance.
		/// </summary>
		public static XYZObjectFactory Instance { get; } = new XYZObjectFactory();

	    /// <summary>
		/// Private constructor, to prevent instantiation.
		/// <seealso cref="Instance"/>
		/// </summary>
		private XYZObjectFactory()
		{
		}

		#region INameInfoFactory Members

		/// <summary>
		/// Retreives the appropriate NameInfo instance for the given domainobject.
		/// </summary>
		/// <param name="domainObject">the domainobject which needs NameInfo</param>
		/// <returns>the NameInfo instance</returns>
		public NameInfo GetNameInfo(object domainObject)
		{
		    return (domainObject as INameInfoSource)?.NameInfo;
		}

		#endregion

		#region IImageInfoFactory Members

		/// <summary>
		/// Retreives the appropriate ImageInfo instance for the given domainobject.
		/// </summary>
		/// <param name="domainObject">the domainobject which needs ImageInfo</param>
		/// <returns>the ImageInfo instance</returns>
		public ImageInfo GetImageInfo(object domainObject)
		{
		    return (domainObject as IImageInfoSource)?.ImageInfo;
		}

		#endregion
	}
}

