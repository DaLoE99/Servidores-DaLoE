using BoomBang_RetroServer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace BoomBang_RetroServer.Game.Spaces
{
    public class Map
    {
        public bool[,] BoolMap;

        public Map(string Map)
        {
            string[] Lines = Map.Split(Convert.ToChar("\n"));
            this.BoolMap = new bool[Lines.Length, Lines[0].Length];
            for (int Y = 0; Y < Lines.Length - 1; Y++)
            {
                for (int X = 0; X < Lines[0].Length; X++)
                {
                    this.BoolMap[Y, X] = (Lines[Y][X] == '0') ? true : false;
                }
            }
        }
        public string GetCoordinatesTakes(Point P, string coors)
        {
            string value = "";
            if (coors != null)
            {
                string[] coordinates = coors.Split(',');
                for (int x = 0; x < coordinates.Length; x++)
                {
                    if (x % 2 == 0)
                    {
                        int i = Convert.ToInt32(coordinates[x]);
                        coordinates[x] = (i + P.X).ToString() + ",";
                    }
                    else
                    {
                        int i = Convert.ToInt32(coordinates[x]);
                        if (x < coordinates.Length - 1)
                        {
                            coordinates[x] = (i + P.Y).ToString() + ",";
                        }
                        else
                        {
                            coordinates[x] = (i + P.Y).ToString();
                        }
                    }
                    value += coordinates[x];
                }
            }
            else { value = null; }

            return value;
        }
        public Point GetCoordinates(int x, int y)
        {
            Point Point;
            int pointx = 0;
            int pointy = 0;
            //CODIGO DE OBJETOS
            Point = new Point(pointx, pointy);
            return Point;
        }
        public Point GetRandomPlace()
        {
            List<Point> Output = new List<Point>();
            for (int Y = 0; Y < this.BoolMap.GetLength(0) - 1; Y++)
            {
                for (int X = 0; X < this.BoolMap.GetLength(1) - 1; X++)
                {
                    if (this.BoolMap[Y, X])
                    {
                        Output.Add(new Point(X, Y));
                    }
                }
            }
            return (Output.Count > 0) ? Output[new Random().Next(0, Output.Count - 1)] : new Point(-1, -1);
        }
        public Point GetPlace()
        {
            List<Point> OutPut = new List<Point>();
            for (int Y = 0; Y < this.BoolMap.GetLength(0) - 1; Y++)
            {
                for (int X = 0; X < this.BoolMap.GetLength(1) - 1; X++)
                {
                    if (this.BoolMap[Y, X])
                    {
                        OutPut.Add(new Point(X, Y));
                    }
                }
            }
            return OutPut;
        }
        public bool IsWalkable(int X, int Y)
        {
            try
            {
                return BoolMap[Y, X];
            }
            catch
            {
                return false;
            }
        }
        public bool IsWalkable(int ActualX, int ActualY, int NextX, int NextY)
        {
            try
            {
                return (IsWalkable(NextX, NextY)) ? (ActualX - NextX == 1 || ActualX - NextX == -1 || ActualX - NextX == 0) ? (ActualY - NextY == 1 || ActualY - NextY == -1 || ActualY - NextY == 0) ? true : false : false : false;
            }
            catch
            {
                return false;
            }
        }
    }
}