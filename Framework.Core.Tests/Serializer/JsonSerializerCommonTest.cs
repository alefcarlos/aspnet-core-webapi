using Framework.Core.Serializer;
using Shouldly;
using Xunit;

namespace Framework.Core.Tests.Serializer
{
    public class JsonSerializerCommonTest
    {
        private readonly JsonSerializerCommon _jsonSerializer;

        public JsonSerializerCommonTest()
        {
            _jsonSerializer = new JsonSerializerCommon();
        }

        [Fact]
        public void Serializer_Success()
        {
            //Arrange
            var model = new Model
            {
                Name = "Alef",
            };

            //Act
            var json = _jsonSerializer.Serialize(model);
            var obj = _jsonSerializer.Deserialize<Model>(json);

            //Assert
            json.ShouldNotBeNullOrWhiteSpace();
            obj.Name.ShouldBe(model.Name);
            obj.Age.ShouldBe(model.Age);
        }

        [Fact]
        public void Deserialize_Success()
        {
            //Arrange
            var model = new Model
            {
                Name = "Alef",
            };

            var json = "{\"name\":\"Alef\"}";

            //Act
            var obj = _jsonSerializer.Deserialize<Model>(json);

            //Assert
            obj.ShouldNotBeNull();
            obj.Name.ShouldBe(model.Name);
            obj.Age.ShouldBe(model.Age);
        }
    }

    public class Model
    {
        public string Name { get; set; }

        public int? Age { get; set; }
    }
}