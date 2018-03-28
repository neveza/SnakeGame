using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class WallBlock
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Color Color;
        public Rectangle rect;

        public void UpdatePosition()
        {
            rect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public WallBlock(Texture2D texture, Vector2 position, Color color)
        {
            Position = position;
            Texture = texture;
            Color = color;

            rect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        }

    }

    class Walls
    {
        //string _key;
        List<WallBlock> _WallList;
        public Dictionary<string, List<WallBlock>> WallDictionary;
        //public List<WallBlock> WallList;

        Texture2D _Texture;

        public void addWalls(string WallGroup, Color color, float StartPointX, float StartPointY, float EndPointX = 0, float EndPointY = 0)
        {
            if (!WallDictionary.ContainsKey(WallGroup))
            {
                _WallList = new List<WallBlock>();
                if (EndPointX == 0 && EndPointY == 0)
                {
                    var wall = new WallBlock(_Texture, new Vector2(StartPointX, StartPointY), color);
                    _WallList.Add(wall);
                }
                else
                {
                    for (float x = StartPointX; x <= EndPointX; x++)
                    {
                        for (float y = StartPointY; y <= EndPointY; y++)
                        {
                            var wall = new WallBlock(_Texture, new Vector2(x, y), color);
                            _WallList.Add(wall);
                        }

                    }
                }
                WallDictionary.Add(WallGroup, _WallList);
                _WallList = new List<WallBlock>();
            }
            else if (WallDictionary.ContainsKey(WallGroup))
            {
                if (EndPointX == 0 && EndPointY == 0)
                {
                    var wall = new WallBlock(_Texture, new Vector2(StartPointX, StartPointY), color);
                    WallDictionary[WallGroup].Add(wall);
                }
                else
                {
                    for (float x = StartPointX; x <= EndPointX; x++)
                    {
                        for (float y = StartPointY; y <= EndPointY; y++)
                        {
                            var wall = new WallBlock(_Texture, new Vector2(x, y), color);
                            WallDictionary[WallGroup].Add(wall);
                        }

                    }
                }
            }

        }

        public void DeleteWalls(string WallGroup)
        {
            WallDictionary.Remove(WallGroup);
        }

        public List<WallBlock> DrawOrder()
        {
            var order = new List<WallBlock>();
            foreach (var key in WallDictionary.Keys)
            {
                foreach (var wall in WallDictionary[key])
                {
                    order.Add(wall);
                }
            }

            return order;
        }

        public Walls(TextureGenerator TextureGen,int Width, int Height)
        {

            _Texture = TextureGen.CreateTexture(Width, Height, Color.White);

            WallDictionary = new Dictionary<string, List<WallBlock>>();

        }
    }
}
