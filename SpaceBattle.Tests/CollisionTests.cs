using Xunit;
using SpaceBattle.Collision;
using SpaceBattle.Commands;
using SpaceBattle.IoC;
using System.Collections.Generic;
using System.IO;

namespace SpaceBattle.Tests
{
    public class CollisionTests
    {
        [Fact]
        public void FeatureVector_CreateVector_StoresDataCorrectly()
        {
            // Arrange
            var features = new double[] { 10.5, 20.3, 5.7 };
            
            // Act
            var vector = new FeatureVector(features, true);
            
            // Assert
            Assert.Equal(features, vector.Features);
            Assert.True(vector.IsCollision);
        }
        
        [Fact]
        public void DecisionTreeBuilder_BuildTree_ReturnsNonNull()
        {
            // Arrange
            var data = new List<FeatureVector>
            {
                new FeatureVector(new double[] { 5, 10 }, true),
                new FeatureVector(new double[] { 15, 20 }, false),
                new FeatureVector(new double[] { 8, 12 }, true),
                new FeatureVector(new double[] { 25, 30 }, false)
            };
            var builder = new DecisionTreeBuilder();
            
            // Act
            var tree = builder.BuildTree(data);
            
            // Assert
            Assert.NotNull(tree);
        }
        
        [Fact]
public void DataFileParser_ParseFile_ReturnsCorrectCount()
{
    // Arrange
    var parser = new DataFileParser();
    string testFile = "test_data.txt";
    
    // Создаем тестовый файл
    File.WriteAllLines(testFile, new[]
    {
        "# комментарий",
        "10.5,20.3,1", 
        "15.7,25.8,0",
        "8.2,12.4,1"
    });
    
    // Act
    var result = parser.ParseFile(testFile);
    
    // Assert
    Assert.Equal(3, result.Count);
    
    // Cleanup
    File.Delete(testFile);
}
        
        [Fact]
public void CheckCollisionCommand_Execute_CompletesWithoutError()
{
    // Arrange
    string dataFile = "test_collision_data.txt";
    
    File.WriteAllLines(dataFile, new[]
    {
        "5.0,10.0,1",
        "15.0,20.0,0", 
        "8.0,12.0,1"
    });
    
    CollisionIoCInitializer.Initialize(dataFile);
    
    var obj1 = new object();
    var obj2 = new object();
    var command = new CheckCollisionCommand(obj1, obj2);
    
    // Act & Assert
    command.Execute();
    
    File.Delete(dataFile);
}
        
        [Fact]
        public void DecisionTreeClassifier_Predict_ReturnsBoolean()
        {
            // Arrange
            var data = new List<FeatureVector>
            {
                new FeatureVector(new double[] { 5 }, true),
                new FeatureVector(new double[] { 15 }, false),
                new FeatureVector(new double[] { 8 }, true),
                new FeatureVector(new double[] { 25 }, false)
            };
            
            var builder = new DecisionTreeBuilder();
            var tree = builder.BuildTree(data);
            var classifier = new DecisionTreeClassifier(tree);
            
            // Act
            var result = classifier.Predict(new double[] { 7 });
            
            // Assert
            Assert.IsType<bool>(result);
        }
    }
}