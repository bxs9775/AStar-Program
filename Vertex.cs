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
	class Vertex
	{
		#region fields
		private double hCost;
		private double gCost;

		private int x;
		private int y;
		private Vertex pathNode;
		List<Vertex> neighbors;


		Rectangle rect;
		#endregion

		#region properties
		/// <summary>
		/// Gets the total cost.
		/// </summary>
		public double FCost
		{
			get { return hCost+gCost; }
		}

		/// <summary>
		/// Gets the estimated cost to end.
		/// </summary>
		public double HCost
		{
			get { return hCost; }
		}

		/// <summary>
		/// Gets the cost from the begining.
		/// </summary>
		public double GCost
		{
			get { return gCost; }
			set { gCost = value; }
		}

		/// <summary>
		/// Gets and sets pathNode.
		/// </summary>
		public Vertex PathNode
		{
			get { return pathNode; }
			set
			{
				pathNode = value;
			}
		}

		/// <summary>
		/// Gets and sets the list of neiboring verticies.
		/// </summary>
		public List<Vertex> Neighbors
		{
			get { return neighbors; }
			set { neighbors = value; }
		}

		/// <summary>
		/// Gets the vertex's onscreen rectangle.
		/// </summary>
		public Rectangle Rect
		{
			get { return rect; }
		}
		#endregion

		/// <summary>
		/// Constructs a vertex.
		/// </summary>
		/// <param name="f">Total cost</param>
		/// <param name="h">Estimated cost to end.</param>
		/// <param name="g">Cost to start.</param>
		public Vertex(int x, int y,double g,double h)
		{
			this.x = x;
			this.y = y;
			rect = new Rectangle(20 * x,20 * y,20,20);

			gCost = g;
			hCost = h;
			neighbors = new List<Vertex>();
		}

		#region operator overloads
		/// <summary>
		/// Finds if one vertex is more than another.
		/// </summary>
		/// <param name="v1">The first vertex</param>
		/// <param name="v2">The second vertex</param>
		/// <returns></returns>
		public static bool operator >(Vertex v1,Vertex v2){
			return v1.FCost > v2.FCost;
		}

		/// <summary>
		/// Finds if one vertex is less than another.
		/// </summary>
		/// <param name="v1">The first vertex</param>
		/// <param name="v2">The second vertex</param>
		/// <returns></returns>
		public static bool operator <(Vertex v1,Vertex v2)
		{
			return v1.FCost < v2.FCost;
		}

		public static bool operator >=(Vertex v1,Vertex v2)
		{
			return (v1.FCost >= v2.FCost);
		}

		public static bool operator <=(Vertex v1,Vertex v2)
		{
			return v1.FCost <= v2.FCost;
		}
		#endregion
	}
}
