// <auto-generated />
namespace BandEngine.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class FixedLatLngInAddressToString : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(FixedLatLngInAddressToString));
        
        string IMigrationMetadata.Id
        {
            get { return "201912060240253_FixedLatLngInAddressToString"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}