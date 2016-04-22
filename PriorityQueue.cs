using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarHomework
{
    class PriorityQueue
    {
        private List<Vertex> queue;

        /// <summary>
        /// Constructs a priority queue.
        /// </summary>
        public PriorityQueue()
        {
            queue = new List<Vertex>();
        }

        /// <summary>
        /// Adds data to the queue.
        /// </summary>
        /// <param name="data">A number to add to the queue.</param>
        public void Enqueue(Vertex data)
        {
            queue.Add(data);
            int index = queue.Count - 1;
            Boolean looping = true;
            int parentIndex;
            while (looping)
            {
                parentIndex = (int)(Math.Floor((double)((index - 1) / 2)));
				if(parentIndex != index && queue[parentIndex] > queue[index])
                {
                    Vertex temp = queue[index];
                    queue[index] = queue[parentIndex];
                    queue[parentIndex] = temp;
                    index = parentIndex;
                }
                else
                {
                    looping = false;
                }
            }
        }

        /// <summary>
        /// Removes the highest priority.
        /// </summary>
        /// <returns>The removed data.</returns>
        public Vertex Dequeue()
        {
            Vertex v = queue[0];
            Delete(0);
            return v;
        }

        /// <summary>
        /// Returns the data with the highest priority.
        /// </summary>
        /// <returns>The data.</returns>
        public Vertex Peek()
        {
            return queue[0];
        }

        /// <summary>
        /// Determines if the queue is empty.
        /// </summary>
        /// <returns>Whether the queue is empty.</returns>
        public bool IsEmpty()
        {
            return (queue.Count == 0);
        }

        /// <summary>
        /// Deletes data at an index.
        /// </summary>
        /// <param name="i">The data to be deleated.</param>
        public void Delete(int i)
        {
            int last = queue.Count - 1;
            queue[i] = queue[last];
            queue.RemoveAt(last);
            bool looping = true;
            while (looping)
            {
                if (((2 * i + 1 < queue.Count) && queue[i] > queue[2 * i + 1]) || ((2 * i + 2 < queue.Count) && queue[i] > queue[2 * i + 2]))
                {
                    int swapIndex;
                    if (!(2*i+2 < queue.Count) || queue[2 * i + 1] < queue[2 * i + 2])
                    {
                        swapIndex = 2 * i + 1;
                    }
                    else
                    {
                        swapIndex = 2 * i + 2;
                    }
                    Vertex temp = queue[i];
                    queue[i] = queue[swapIndex];
                    queue[swapIndex] = temp;
                    i = swapIndex;
                }
                else
                {
                    looping = false;
                }
            }
        }

		public void Delete(Vertex v)
		{
			for(int i = 0; i < queue.Count; i++)
			{
				if(queue[i] == v)
				{
					Delete(i);
					return;
				}
			}
		}

		/// <summary>
		/// Checks if the PriorityQueue contains the requested value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(Vertex value)
		{
			return queue.Contains(value);
		}
    }
}
