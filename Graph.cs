using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace AStarHomework
{
	class Graph
	{
		#region fields
		private Random rng;
		private Texture2D tile;

		private Vertex[,] verticies;
		private Vertex startVertex;
		private Vertex endVertex;

		private PriorityQueue openList;
		private List<Vertex> closedList;

		private List<Vertex> path;
		#endregion

		public Graph(Texture2D tile)
		{
			this.tile = tile;
			
			verticies = new Vertex[40,24];

			rng = new Random();
			int startX = rng.Next(0, verticies.GetLength(0));
			int startY = rng.Next(0, verticies.GetLength(1));
			int endX = 0;
			int endY = 0;
			do{
				endX = rng.Next(0, verticies.GetLength(1));
				endY = rng.Next(0, verticies.GetLength(1));
			} while(startX == endX && startY == endY);

			Rectangle startRect = new Rectangle(20 * startX,20 * startY,20,20);
			Point startCenter = startRect.Center;
			Rectangle endRect = new Rectangle(20 * endX,20 * endY,20,20);
			Point endCenter = endRect.Center;
			Rectangle currentRect;
			Point currentCenter;
			
			double gCost = 0; //cost from start.
			double hCost = 0; //cost to end.
			for(int x = 0; x < verticies.GetLength(0); x++)
			{
				for(int y = 0; y < verticies.GetLength(1); y++)
				{
					currentRect = new Rectangle(20 * x,20 * y,20,20);
					currentCenter = currentRect.Center;

					gCost = int.MaxValue;/*Math.Sqrt(Math.Pow(currentCenter.X - startCenter.X,2) + Math.Pow(currentCenter.Y - startCenter.Y,2));*/
					hCost = Math.Sqrt(Math.Pow(currentCenter.X - endCenter.X,2) + Math.Pow(currentCenter.Y - endCenter.Y,2));

					verticies[x,y] = new Vertex(x,y,gCost,hCost);
					if(x == startX && y == startY)
					{
						startVertex = verticies[x,y];
					} else if(x == endX && y == endY)
					{
						endVertex = verticies[x,y];
					}
					
				}
			}

			//Finds nieghbors.
			for(int x = 0; x < verticies.GetLength(0); x++)
			{
				for(int y = 0; y < verticies.GetLength(1); y++)
				{
					verticies[x,y].Neighbors = FindNeighbors(x,y);
				}
			}

			openList = new PriorityQueue();
			openList.Enqueue(startVertex);

			closedList = new List<Vertex>();

			path = new List<Vertex>();
		}

		public void Update()
		{
			AStar();
			path.Clear();
			Vertex current = endVertex;
			path.Add(current);
			while(current.PathNode != null)
			{
				current = current.PathNode;
				path.Add(current);
			}
		}

		/// <summary>
		/// Draws the graph.
		/// </summary>
		/// <param name="game">The space the graph is in.</param>
		/// <param name="spriteBatch">Draws the graph to the screen.</param>
		public void Draw(Game1 game, SpriteBatch spriteBatch)
		{
			Color tileColor = Color.White;

			for(int x = 0; x < verticies.GetLength(0); x++)
			{
				for(int y = 0; y < verticies.GetLength(1); y++)
				{
					tileColor = Color.White;
					if(verticies[x,y] == startVertex)
					{
						tileColor = Color.Green;
					} else if(verticies[x,y] == endVertex)
					{
						tileColor = Color.Red;
					} else if(path.Contains(verticies[x,y]))
					{
						tileColor = Color.Yellow;
					} else if(openList.Contains(verticies[x,y]))
					{
						tileColor = Color.LightGreen;
					} else if(closedList.Contains(verticies[x,y]))
					{
						tileColor = Color.LightPink;
					}
					spriteBatch.Draw(tile,verticies[x,y].Rect,tileColor);
				}
			}
		}

		/// <summary>
		/// Runs through one step of A* pathfinding.
		/// </summary>
		public void AStar()
		{
			Vertex current;
			
			Point currentCenter;
			Point nieghborCenter;

			if(openList.Peek() != endVertex)
			{
				current = openList.Dequeue();
				currentCenter = current.Rect.Center;
				closedList.Add(current);

				foreach(Vertex nieghbor in current.Neighbors)
				{
					nieghborCenter = nieghbor.Rect.Center;
					double costBetween = Math.Sqrt(Math.Pow(nieghborCenter.X - currentCenter.X,2) + Math.Pow(nieghborCenter.Y - currentCenter.Y,2));
					double cost = current.GCost + costBetween;
					if(openList.Contains(nieghbor) && cost < nieghbor.GCost)
					{
						openList.Delete(nieghbor);
					}
					if(closedList.Contains(nieghbor) && cost < nieghbor.GCost)
					{
						closedList.Remove(nieghbor);
					}
					if(!(openList.Contains(nieghbor) || closedList.Contains(nieghbor)))
					{
						nieghbor.GCost = cost;
						openList.Enqueue(nieghbor);
						nieghbor.PathNode = current;
					}
				}
			}
		}

		/// <summary>
		/// Finds the neighbors of a vertex.
		/// </summary>
		/// <param name="x">The starting vertex's x.</param>
		/// <param name="y">The starting vertex's y.</param>
		/// <returns></returns>
		private List<Vertex> FindNeighbors(int x,int y)
		{
			List<Vertex> neighbors = new List<Vertex>();
			if(x >= 0 && x < verticies.GetLength(0) && y >= 0 && y < verticies.GetLength(1))
			{
				for(int i = x - 1; i < x + 2; i++)
				{
					for(int j = y - 1; j < y + 2; j++)
					{
						if((i >= 0 && i < verticies.GetLength(0) && j >= 0 && j < verticies.GetLength(1)) && !(i == x && j == y))
						{
							neighbors.Add(verticies[i,j]);
						}
					}
				}
			}
			return neighbors;
		}
	}
}
