using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NUnit.Framework;

namespace NetTopologySuite.Samples.Tests.Github
{
    [TestFixture]
    public class Issue96Fixture
    {
        [Test]
        public void linearring_should_be_written_as_wkb()
        {
            var factory = GeometryFactory.Default;
            var expected = factory.CreateLinearRing(new[]
            {
                new Coordinate(0, 0),
                new Coordinate(10, 0),
                new Coordinate(10, 10),
                new Coordinate(0, 10),
                new Coordinate(0, 0)
            });

            var writer  = new WKBWriter();
            byte[] bytes =  writer.Write(expected);
            Assert.That(bytes, Is.Not.Null);
            Assert.That(bytes, Is.Not.Empty);

            var reader = new WKBReader();
            var actual = reader.Read(bytes);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(actual.OgcGeometryType, Is.EqualTo(expected.OgcGeometryType));

            // WKBReader reads "LinearRing" geometries as LineString
            Assert.That(expected, Is.InstanceOf<LinearRing>());
            Assert.That(actual, Is.InstanceOf<LineString>());
            Assert.That(actual.GeometryType, Is.Not.EqualTo(expected.GeometryType));
        }
    }
}