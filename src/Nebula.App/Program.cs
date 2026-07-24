using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using Nebula.Core;

namespace Nebula.App;

class Program
{
    static int Main(string[] args)
    {
        // Define command line options
        var rootCommand = new RootCommand("Generates a procedural nebula background image.");

        var widthOption = new Option<int>(new[] {"--width", "-w"}, "Width of the image in pixels") { IsRequired = true };
        var heightOption = new Option<int>(new[] {"--height", "-h"}, "Height of the image in pixels") { IsRequired = true };
        var starCountOption = new Option<int>(new[] {"--stars", "-s"}, "Number of stars to generate") { IsRequired = false, DefaultValue = 200 };
        var outputOption = new Option<FileInfo>(new[] {"--output", "-o"}, "Output file path (PNG format)") { IsRequired = true };

        // Nebula color options (as hex string, e.g., "FF00FF" for magenta)
        var colorOption = new Option<string>(new[] {"--color", "-c"}, "Nebula color in hex format (RRGGBB, default: 320064)") { IsRequired = false, DefaultValue = "320064" };
        var scaleOption = new Option<float>(new[] {"--scale"}, "Nebula noise scale (default: 0.01)") { IsRequired = false, DefaultValue = 0.01f };
        var intensityOption = new Option<float>(new[] {"--intensity", "-i"}, "Nebula intensity (0.0 to 1.0, default: 0.5)") { IsRequired = false, DefaultValue = 0.5f };

        rootCommand.AddOption(widthOption);
        rootCommand.AddOption(heightOption);
        rootCommand.AddOption(starCountOption);
        rootCommand.AddOption(outputOption);
        rootCommand.AddOption(colorOption);
        rootCommand.AddOption(scaleOption);
        rootCommand.AddOption(intensityOption);

        rootCommand.SetHandler((int width, int height, int starCount, FileInfo output, string colorHex, float scale, float intensity) =>
        {
            try
            {
                // Parse color from hex
                if (!System.Text.RegularExpressions.Regex.IsMatch(colorHex, "^[0-9A-Fa-f]{6}$"))
                {
                    Console.Error.WriteLine("Error: Color must be a 6-digit hex string (e.g., FF00FF for magenta).");
                    return 1;
                }

                byte r = Convert.ToByte(colorHex.Substring(0, 2), 16);
                byte g = Convert.ToByte(colorHex.Substring(2, 2), 16);
                byte b = Convert.ToByte(colorHex.Substring(4, 2), 16);
                var nebulaColor = new Rgba32(r, g, b, 255);

                // Validate intensity
                if (intensity < 0.0f || intensity > 1.0f)
                {
                    Console.Error.WriteLine("Error: Intensity must be between 0.0 and 1.0.");
                    return 1;
                }

                // Generate the image
                var generator = new NebulaGenerator();
                using var image = generator.Generate(width, height, starCount, nebulaColor, scale, intensity);

                // Save the image as PNG
                image.Save(output.FullName, new PngEncoder());

                Console.WriteLine($"Successfully generated nebula image: {output.FullName}");
                Console.WriteLine($"Dimensions: {width}x{height}");
                Console.WriteLine($"Stars: {starCount}");
                Console.WriteLine($"Color: #{colorHex}");
                Console.WriteLine($"Scale: {scale}");
                Console.WriteLine($"Intensity: {intensity}");

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }, widthOption, heightOption, starCountOption, outputOption, colorOption, scaleOption, intensityOption);

        return rootCommand.Invoke(args);
    }
}