using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._15_UITrees
{
	/// <summary>
	/// TreeItems usually appear in the Petrel Input tree, as a leaf.
	/// (cannot contain more elements under itself)
	/// </summary>
	class XYZObject : INameInfoSource, IImageInfoSource
	{
	    private float _radius = 30.0f;
	    private float _z = 100.0f;
	    private float _y = 100.0f;
	    private float _x = 100.0f;


	    public XYZObject()
		{
		}

	    public float X
	    {
	        get { return _x; }
	        set { _x = value; }
	    }

	    public float Y
	    {
	        get { return _y; }
	        set { _y = value; }
	    }

	    public float Z
	    {
	        get { return _z; }
	        set { _z = value; }
	    }

	    public float Radius
	    {
	        get { return _radius; }
	        set { _radius = value; }
	    }

	    public string Name { get; set; } = "XYZ Object";

	    #region INameInfoSource Members

		public NameInfo NameInfo => new XYZObjectNameInfo(this);

	    #endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo => new XYZObjectImageInfo();

	    #endregion
	}

}
