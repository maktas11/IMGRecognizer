using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;
using System.Threading.Tasks;

namespace ImageDetector
{
    public class Renderer : Overlay
    {
        ImDrawListPtr drawListPTr; // add shapes
        private List<Rectangle2D> matches = new List<Rectangle2D>(); // matches
        protected override void Render()
        {
            ImGui.Begin("Menu");
            DrawOverlay();
            DrawBoxes();
        }

        void DrawOverlay()
        {
            ImGui.SetNextWindowSize(new Vector2(1920, 1080));
            ImGui.SetNextWindowPos(new Vector2(0, 0));

            ImGui.Begin("overlay", ImGuiWindowFlags.NoDecoration
                  | ImGuiWindowFlags.NoBackground
                  | ImGuiWindowFlags.NoBringToFrontOnFocus
                  | ImGuiWindowFlags.NoMove
                  | ImGuiWindowFlags.NoInputs
                  | ImGuiWindowFlags.NoCollapse
                  | ImGuiWindowFlags.NoScrollbar
                  | ImGuiWindowFlags.NoScrollWithMouse
                  );
        }

        void DrawBoxes()
        {
            drawListPTr = ImGui.GetWindowDrawList();
            foreach (Rectangle2D match in matches)
            {
                drawListPTr.AddRect(match.origin, match.end, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 0, 0, 1)));
                drawListPTr.AddText(new Vector2(match.origin.X, match.end.Y), ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 1)), $"Picture Found {match.origin}");
            }
        }

        public void AddMatch(Rectangle2D match)
        {
            bool isAlreadyPresent = false;

            foreach(var item in matches )
            {
                if (item.origin.X == match.origin.X && item.origin.Y == match.origin.Y)
                { 
                    isAlreadyPresent = true;
                    break;
                }
            }
            if (!isAlreadyPresent )
            {
                matches.Clear();
                matches.Add(match);
            }
        }

    }
}
