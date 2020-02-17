using Slb.Ocean.Core;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.Data.Persistence;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._15_UITrees
{
	/// <summary>
	/// TreeItems usually appear in the Petrel Input tree, as a leaf.
	/// (cannot contain more elements under itself)
	/// </summary>
	[Archivable]
	class XYZObject : INameInfoSource, IImageInfoSource, IIdentifiable
	{
        [Archived]
	    private float _radius = 30.0f;

        [Archived]
	    private float _z = 100.0f;

	    [Archived]
	    private float _y = 100.0f;

	    [Archived]
	    private float _x = 100.0f;

	    [Archived]
	    private Droid _droid;

	    [Archived]
	    private string _name = "XYZ Object";

	    [ArchivableContextInject]
	    private StructuredArchiveDataSource _structuredArchiveDataSource;


	    public XYZObject()
	    {
	        var dataSourceManager = DataManager.DataSourceManager;
	        _structuredArchiveDataSource = XYZOjectDataSourceFactory.Get(dataSourceManager);
            //
	        _droid = _structuredArchiveDataSource.GenerateDroid();
	        _structuredArchiveDataSource.AddItem(_droid, this);
	    }

	    public float X
	    {
	        get { return _x; }
	        set
	        {
	            _x = value;
	            _structuredArchiveDataSource.IsDirty = true;
	        }
	    }

	    public float Y
	    {
	        get { return _y; }
	        set
	        {
	            _y = value;
	            _structuredArchiveDataSource.IsDirty = true;
            }
        }

	    public float Z
	    {
	        get { return _z; }
	        set
	        {
	            _z = value;
	            _structuredArchiveDataSource.IsDirty = true;
            }
        }

	    public float Radius
	    {
	        get { return _radius; }
	        set
	        {
	            _radius = value;
	            _structuredArchiveDataSource.IsDirty = true;
            }
        }

	    public string Name
	    {
	        get { return _name; }
	        set
	        {
	            _name = value;
	            _structuredArchiveDataSource.IsDirty = true;
            }
	    }

	    #region INameInfoSource Members

		public NameInfo NameInfo => new XYZObjectNameInfo(this);

	    #endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo => new XYZObjectImageInfo();

	    #endregion

	    public Droid Droid => _droid;
	}

}
