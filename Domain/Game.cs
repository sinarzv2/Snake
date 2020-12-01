using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Game
    {
        public Game()
        {
            Board = new Board();
        }
        public Board Board { get; set; }
    }
}
