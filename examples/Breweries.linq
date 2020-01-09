<Query Kind="Program">
  <Reference Relative="..\src\Gexf\bin\Debug\netstandard2.0\Gexf.dll">D:\Code\gexf-dotnet\src\Gexf\bin\Debug\netstandard2.0\Gexf.dll</Reference>
  <Namespace>Gexf</Namespace>
  <Namespace>Gexf.Visualization</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
</Query>

void Main()
{
	ObjectIDGenerator idGenerator = new ObjectIDGenerator();

	GexfId UniqueId(Location loc)
	{
		return (int)idGenerator.GetId(loc, out bool _);
	}

	var dragon = new Location(40.589649m, -105.045624m, "Horse & Dragon Brewing Company");
	var odell = new Location(40.589476m, -105.063186m, "Odell Brewing Company");
	var belgium = new Location(40.593238m, -105.068600m, "New Belgium Brewing Company");
	var equinox = new Location(40.586356m, -105.075812m, "Equinox Brewing");

	var locations = new[] { dragon, odell, belgium, equinox };

	var gexf = new GexfDocument();
	gexf.Meta.LastModified = DateTimeOffset.Now;
	gexf.Meta.Creator = Environment.UserName;

	gexf.Graph.IdType = GexfIdType.Integer;

	GexfId lat = "lat";
	GexfId lon = "lon";

	gexf.Graph.NodeAttributes.AddRange(new[]
	{
	   new GexfAttribute(lat)
	   {
		   Title = "latitude",
		   Type = GexfDataType.Double
	   },
	   new GexfAttribute(lon)
	   {
		   Title = "longitude",
		   Type = GexfDataType.Double
	   }
	});

	foreach (var location in locations)
	{
		var node = new GexfNode(UniqueId(location))
		{
			Label = location.Label,
			AttrValues =
			{
				new GexfAttributeValue(lat, location.Lat),
				new GexfAttributeValue(lon, location.Lon),
			}
		};
		//node.Size(1.3);

		gexf.Graph.Nodes.Add(node);
	};

	gexf.Graph.Edges.AddRange(
		new GexfEdge(1, UniqueId(dragon), UniqueId(odell)),
		new GexfEdge(2, UniqueId(odell), UniqueId(belgium)),
		new GexfEdge(3, UniqueId(belgium), UniqueId(equinox))
	);

	string path = Path.Combine(
		Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
		"breweries.gexf");
	gexf.Save(path);
}

// Define other methods and classes here
class Location
{
	public Location(decimal lat, decimal lon, string label)
	{
		Lat = lat;
		Lon = lon;
		Label = label;
	}

	public decimal Lat { get; }
	public decimal Lon { get; }
	public string Label { get; }
}