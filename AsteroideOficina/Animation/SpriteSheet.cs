using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteSheet
{
	public class SpriteSheet
	{
		private readonly string _assetName;
		private readonly int _frameHeight;
		private readonly int _frameWidth;
		private readonly int _framesInX;
		private readonly Texture2D[] _frameTextures;
		private readonly int _totalFrames;

		private bool _loop;
		private bool _isFinished;

		private Texture2D _spriteSheetTexture;

		public SpriteSheet(string assetName, int frameWidth, int frameHeight, int framesInX, int totalFrames,
			bool loop = true)
		{
			_assetName = assetName;
			_frameWidth = frameWidth;
			_frameHeight = frameHeight;
			_framesInX = framesInX;
			_totalFrames = totalFrames;
			_loop = loop;
			_isFinished = false;

			_frameTextures = new Texture2D[totalFrames];

			Frame = 0;
		}

		//The current frame of our sprite sheet
		public int Frame { get; private set; }

		//Used for drawing and eventually per-pixel collision
		public Texture2D CurrentFrame => _frameTextures[Frame];

		/// <summary>
		/// Loads the main texture containing all frames and loads the texture array for each individual frame.
		/// </summary>
		/// <param name="content">The Content Manager for our game</param>
		/// <param name="graphicsDevice">The Graphics Device for our game</param>
		public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
		{
			if (string.IsNullOrEmpty(_assetName)) throw new NullReferenceException("_assetName must not be null or empty");

			_spriteSheetTexture = content.Load<Texture2D>(_assetName);
			BreakSheetIntoFrames(graphicsDevice);
		}

		/// <summary>
		/// Breaks the single texture containing multiple frames into multiple textures
		/// </summary>
		/// <param name="graphicsDevice">The Graphics Device for our game</param>
		private void BreakSheetIntoFrames(GraphicsDevice graphicsDevice)
		{
			Color[,] spriteSheetTextureData = new Color[_spriteSheetTexture.Width, _spriteSheetTexture.Height];
            _spriteSheetTexture.GetData(spriteSheetTextureData);

			for (int currentFrame = 0; currentFrame < _totalFrames; currentFrame++)
			{
				_frameTextures[currentFrame] = GetTextureFromFrame(spriteSheetTextureData, currentFrame, graphicsDevice);
			}
		}

		/// <summary>
		/// Gets a single texture at the frame index from the texture that has all frames
		/// </summary>
		/// <param name="spriteSheetTextureData">The color data of the entire texture that contains all frames</param>
		/// <param name="currentFrame">The current frame we want to retrieve</param>
		/// <param name="graphicsDevice">The Graphics Device for our game</param>
		/// <returns>A texture that represents the provided frame</returns>
		private Texture2D GetTextureFromFrame(Color[,] spriteSheetTextureData, int currentFrame, GraphicsDevice graphicsDevice)
		{
			Color[] frameColorData = new Color[_frameWidth * _frameHeight];
			Texture2D frameTexture = new Texture2D(graphicsDevice, _frameWidth, _frameHeight);

			int fX = 0, fY = 0;

			int x = currentFrame % _framesInX * _frameWidth;
			int y = currentFrame / _framesInX * _frameHeight;

			int endX = x + _frameWidth;
			int endY = y + _frameHeight;

			for (int colorY = y; colorY < endY; colorY++)
			{
				for (int colorX = x; colorX < endX; colorX++)
				{
					frameColorData[fX + fY * _frameWidth] = spriteSheetTextureData[colorX, colorY];

					fX++;
				}

				fY++;
				fX = 0;
			}

			frameTexture.SetData(frameColorData);
			return frameTexture;
		}

		/// <summary>
		/// Advance the frames and loop if allowed.
		/// </summary>
		/// <param name="gameTime">Our Game Time of our game</param>
		public void Update(GameTime gameTime)
		{
			if (_isFinished) return;

			Frame++;
			if (Frame >= _totalFrames)
			{
				if (_loop)
				{
					Frame = 0;
				}
				else
				{
					Frame = _totalFrames - 1;
					_isFinished = true;
				}
			}
		}
	}
}
