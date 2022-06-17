using System;
using System.IO;

namespace main
{
    public class ListNode
    {
        public Position position;
        public ListNode next;

        public ListNode(Position position, ListNode next)
        {
            this.position = position;
            this.next = next;
        }
    }

    public class List
    {
        private readonly ListNode _head;
        private ListNode _tail;

        public List()
        {
            Position headPosition = new Position(-1, -1);
            _head = new ListNode(headPosition, null);
            _tail = _head;
        }

        public void Prepend(Position position)
        {
            ListNode node = new ListNode(position, _head.next);
            _head.next = node;

            if (node.next == null)
            {
                _tail = node;
            }
        }

        public void Show()
        {
            ListNode node = _head.next;

            Console.Write("\t");
            while (node != null)
            {
                Console.Write($" →  [{node.position.Column},{node.position.Row}]");
                node = node.next;
            }

            Console.WriteLine();
        }

        public void SaveFile(string fileName = "maze-result.txt")
        {
            using (StreamWriter stream = new StreamWriter(fileName))
            {
                ListNode node = _head.next;

                while (node != null)
                {
                    stream.Write($"({node.position.Column},{node.position.Row}) ");
                    node = node.next;
                }
            }

            Console.WriteLine("\n\tFile saved!!!");
        }
    }
}
