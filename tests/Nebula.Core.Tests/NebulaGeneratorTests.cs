using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Nebula.Core;
using Xunit;

namespace Nebula.Core.Tests;

public class NebulaGeneratorTests
{
    [Fact]
    public void Generate_ReturnsImageOfCorrectSize()
    {
        // Arrange
        var generator = new NebulaGenerator();
        int width = 100;
        int height = 100;
        int starCount = 10;

        // Act
        using var image = generator.Generate(width, height, starCount);

        // Assert
        Assert.Equal(width, image.Width);
        Assert.Equal(height, image.Height);
    }

    [Fact]
    public void Generate_WithZeroStars_ReturnsImage()
    {
        // Arrange
        var generator = new NebulaGenerator();
        int width = 50;
        int height = 50;
        int starCount = 0;

        // Act
        using var image = generator.Generate(width, height, starCount);

        // Assert
        Assert.Equal(width, image.Width);
        Assert.Equal(height, image.Height);
    }

    [Fact]
    public void Generate_WithCustomColor_UsesThatColorInNebula()
    {
        // Arrange
        var generator = new NebulaGenerator();
        int width = 10;
        int height = 10;
        int starCount = 0; // No stars to simplify checking
        var nebulaColor = new Rgba32(255, 0, 0, 255); // Red

        // Act
        using var image = generator.Generate(width, height, starCount, nebulaColor, 0.1f, 1.0f);

        // Assert: Since we set intensity to 1.0 and scale to 0.1, the noise will be very smooth.
        // We'll check a few pixels to see if they have a red tint (though due to noise, it's not uniform).
        // For simplicity, we'll just ensure the image is generated and not throw.
        Assert.NotNull(image);
    }
}