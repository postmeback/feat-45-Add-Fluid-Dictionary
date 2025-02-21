namespace TryAtSoftware.Extensions.Collections.Tests;


using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class FluidDictionaryTests
{
    [Fact]
    public void SetAndGet()
    {
        // Arrange
        var dictionary = new FluidDictionary<string>();
        var randomValue = RandomizationHelper.RandomInteger(0, 1000);
        dictionary.Set("key1", randomValue);

        // Act
        var success = dictionary.TryGetValue("key1", out object value);

        // Assert
        Assert.True(success);
        Assert.Equal(randomValue, value); // Ensure that the retrieved value is the same as the one we set.
    }


    [Fact]
    public void GetRequiredValue_KeyNotFound()
    {
        // Arrange
        var dictionary = new FluidDictionary<string>();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => dictionary.GetRequiredValue<int>("key1"));
    }

    [Fact]
    public void GetRequiredValue_InvalidValueType()
    {
        // Arrange
        var dictionary = new FluidDictionary<string>();
        dictionary.Set("key1", "value");

        // Act & Assert
        Assert.Throws<InvalidCastException>(() => dictionary.GetRequiredValue<int>("key1"));
    }

    [Fact]
    public void GetKeys()
    {
        // Arrange
        var dictionary = new FluidDictionary<string>();
        dictionary.Set("key1", 1);
        dictionary.Set("key2", 2);
        dictionary.Set("key3", 3);

        // Act
        var keys = dictionary.Keys.ToList();

        // Assert
        Assert.Contains("key1", keys);
        Assert.Contains("key2", keys);
        Assert.Contains("key3", keys);
        Assert.Equal(3, keys.Count);
    }

    [Fact]
    public void Remove_KeyNotFound()
    {
        // Arrange
        var dictionary = new FluidDictionary<string>();

        // Act
        bool success = dictionary.Remove("key1");

        // Assert
        Assert.False(success);
    }
}
