using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;

[Library]
public partial class SpawnList : Panel
{
	VirtualScrollPanel Canvas;

	public SpawnList()
	{
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemWidth = 100;
		Canvas.Layout.ItemHeight = 100;

		Canvas.OnCreateCell = ( cell, data ) =>
		{
			var file = (string)data;
			var name = file;
			name = name.Substring( 0, name.IndexOf( "." ));
			while ( name.IndexOf( '/' ) != -1 )
			{
				name = name.Substring( name.IndexOf( '/' ) + 1 );
			}
			var panel = cell.Add.Button( name );
			panel.AddClass( "icon" );
			panel.AddEventListener( "onclick", () => ConsoleSystem.Run( "spawn", "models/" + file ) );
			panel.Style.BackgroundImage = Texture.Load( FileSystem.Mounted, $"/models/{file}_c.png", false );
		};

		foreach ( var file in FileSystem.Mounted.FindFile( "models", "*.vmdl_c.png", true ) )
		{
			if ( string.IsNullOrWhiteSpace( file ) ) continue;
			if ( file.Contains( "_lod0" ) ) continue;
			if ( file.Contains( "clothes" ) ) continue;

			Canvas.AddItem( file.Remove( file.Length - 6 ) );
		}
	}
}
