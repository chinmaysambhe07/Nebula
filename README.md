# 🌌 Nebula: Procedural Space Background Generator

Generate stunning procedural space backgrounds (nebulae with stars) using Perlin noise - perfect for game backgrounds, wallpapers, or UI elements!

## ✨ Features

- **🎨 Procedural Generation**: Creates unique nebula backgrounds every time using Perlin noise for beautiful cloud-like structures
- **⚙️ Highly Customizable**: Adjust image size, star count, nebula color, scale, and intensity via command-line arguments
- **🖼️ High-Quality Output**: Generates crisp PNG images suitable for game backgrounds, wallpapers, or UI elements
- **💻 Cross-Platform**: Built with .NET 6.0 and uses the pure-managed ImageSharp library for image processing
- **📜 MIT Licensed**: Free for personal, educational, and commercial use

## 🚀 Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later

### Building from Source

1. **Clone this repository**  
   `git clone https://github.com/yourusername/Nebula.git`
2. **Build the solution:**  
   ```bash
   dotnet build
   ```

### Usage

The console application (`Nebula.App`) generates a nebula background image and saves it as a PNG file.

#### Example Command

```bash
dotnet run --project src/Nebula.App -- \
  --width 1920 \
  --height 1080 \
  --stars 300 \
  --color FF4500 \
  --scale 0.015 \
  --intensity 0.6 \
  --output ./nebula-background.png
```

#### Command-Line Options

| Option | Alias | Description | Required | Default |
|--------|-------|-------------|----------|---------|
| `--width` | `-w` | Width of the image in pixels | Yes | - |
| `--height` | `-h` | Height of the image in pixels | Yes | - |
| `--stars` | `-s` | Number of stars to generate | No | 200 |
| `--output` | `-o` | Output file path (PNG format) | Yes | - |
| `--color` | `-c` | Nebula color in hex format (RRGGBB) | No | `320064` (purple) |
| `--scale` | - | Nebula noise scale (higher = more zoomed out) | No | 0.01 |
| `--intensity` | `-i` | Nebula intensity (0.0 to 1.0) | No | 0.5 |

### Library Usage

You can also use the `Nebula.Core` library in your own .NET projects:

```csharp
using Nebula.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

var generator = new NebulaGenerator();
using var image = generator.Generate(
    width: 800,
    height: 600,
    starCount: 150,
    nebulaColor: new Rgba32(0, 25, 100, 255), // Dark blue nebula
    scale: 0.02f,
    intensity: 0.4f);

image.Save("my-nebula.png", new PngEncoder());
```

## 🔭 How It Works

The generator combines two main cosmic elements:

1. **🌫️ Nebula Clouds**: Created using 2D Perlin noise to produce smooth, cloud-like patterns. The noise values are blended with a specified color to form the nebula.
2. **⭐ Stars**: Randomly placed white-to-yellow points of varying brightness to simulate distant stars.

## 🎨 Customization

- **🎨 Color**: Specify the nebula color as a 6-digit hex string (e.g., `FF00FF` for magenta, `00FFFF` for cyan).
- **🔍 Scale**: Controls the frequency of the noise pattern. Lower values create larger, smoother clouds; higher values create more detailed, turbulent patterns.
- **💫 Intensity**: Adjusts how strongly the nebula color affects the background (0.0 = no nebula, 1.0 = full color intensity).

## 📋 Requirements

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- SixLabors.ImageSharp (via NuGet, automatically restored)

## 📜 License

This project is licensed under the [MIT License](LICENSE).

## 🙏 Acknowledgements

- [SixLabors.ImageSharp](https://sixlabors.com/products/imagesharp/) - For the incredible image processing library.
- The original Perlin noise implementation by Stefan Gustavson (stegu@itn.liu.se).

*Ready to add some cosmic flair to your projects? Generate your first nebula today!* 🚀
