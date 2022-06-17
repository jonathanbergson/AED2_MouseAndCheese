using System;

namespace main
{
    public class PileNode
    {
        public Position Position { get; }
        public PileNode Next { get; }

        public PileNode(Position position, PileNode next)
        {
            Position = position;
            Next = next;
        }
    }

    internal class Pile
    {
        private PileNode _top;
        private int _count;

        public Pile()
        {
            _top = null;
            _count = 0;
        }

        public int Count => _count;

        public void Add(Position position)
        {
            PileNode node = new PileNode(position, _top);
            _top = node;
            _count++;
        }

        public Position Remove()
        {
            if (_top == null)
            {
                throw new Exception("Pilha vazia. Nenhum elemento para remover.");
            }

            Position removed = _top.Position;
            _top = _top.Next;
            _count--;

            return removed;
        }

        public bool Isset(Position position)
        {
            PileNode node = _top;
            bool inset = false;

            while (node != null)
            {
                if (position.Column == node.Position.Column && position.Row == node.Position.Row)
                {
                    inset = true;
                    break;
                }

                node = node.Next;
            }

            return inset;
        }

        public void Show()
        {
            Console.WriteLine("\n\tPile path:");
            Console.WriteLine("\t  [ ");
            for (PileNode node = _top; node != null; node = node.Next)
            {
                Console.Write("\t");
                Console.Write("    ");
                Console.WriteLine($"{node.Position.Column}, {node.Position.Row}");
            }

            Console.WriteLine("\t]");
        }
    }
}
