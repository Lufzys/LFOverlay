using LFOverlay.Cheat;
using LFOverlay.Cheat.SDK;
using LFOverlay.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memorys;
using LFOverlay.Cheat.SDK.Variables;
using LFOverlay.Cheat.SDK.Entites;
using System.Numerics;
using System.Diagnostics;

namespace LFOverlay
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static Thread thRender = new Thread(new ThreadStart(RenderThread));
        public static Thread thOverlayUpdate = new Thread(new ThreadStart(OverlayUpdate));
        private void MainForm_Load(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().PriorityBoostEnabled = true;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            Overlay.Initalize();
            Game.Initalize();
            thRender.Start();
            thOverlayUpdate.Start();
        }

        public static void RenderThread()
        {
            while (Game.Initalized)
            {
                int ClientState = Memory.Read<int>(Game.Engine + Offsets.signatures.dwClientState);
                Enums.SignOnState ClientState_State = (Enums.SignOnState)Memory.Read<int>(ClientState + Offsets.signatures.dwClientState_State);
                if(ClientState_State == Enums.SignOnState.FULL)
                {
                    float[] Viewmatrix = Memory.ReadMatrix<float>(Game.Client + Offsets.signatures.dwViewMatrix, 16);
                    int ClientState_MaxPlayer = Memory.Read<int>(ClientState + Offsets.signatures.dwClientState_MaxPlayer);

                    Entity LocalPlayer = new Entity(Memory.Read<int>(Game.Client + Offsets.signatures.dwLocalPlayer));
                    if(true)
                    {
                        int x = Overlay.Width / 2;
                        int y = Overlay.Height / 2;

                        int dx = Overlay.Width / 90;
                        int dy = Overlay.Height / 90;

                        x -= (int)(dx * (LocalPlayer.PunchAngle.Y));
                        y += (int)(dy * (LocalPlayer.PunchAngle.X));

                        Overlay.FilledRectangle(x - 8, y, 16, 1, Color.LimeGreen);
                        Overlay.FilledRectangle(x, y - 8, 1, 16, Color.LimeGreen);
                    }

                    for(int i = 0; i < ClientState_MaxPlayer; i++)
                    {
                        Entity entity = new Entity(Memory.Read<int>(Game.Client + Offsets.signatures.dwEntityList + i * 0x10));
                        if (!entity) continue;
                        if (entity.Address == LocalPlayer.Address) continue;
                        if (entity.Dormant) continue;
                        if (entity.Health <= 0) continue;

                        if (entity.Team == Enums.Team.None || entity.Team == Enums.Team.Spectator) continue;

                        Vector2 OriginPosition2D, HeadPosition2D;
                        if (Memory.WorldToScreen(entity.Origin, out OriginPosition2D, Viewmatrix, Overlay.Width, Overlay.Height) && Memory.WorldToScreen(entity.Bone(8), out HeadPosition2D, Viewmatrix, Overlay.Width, Overlay.Height))
                        {
                            OriginPosition2D = new Vector2(OriginPosition2D.X, OriginPosition2D.Y);
                            HeadPosition2D = new Vector2(HeadPosition2D.X, HeadPosition2D.Y);
                            float BoxHeight = OriginPosition2D.Y - HeadPosition2D.Y;
                            float BoxWidth = BoxHeight / 2.4f;

                            float x1 = HeadPosition2D.X - (BoxWidth / 2f);
                            float y1 = HeadPosition2D.Y;

                            Overlay.CornerBox((int)x1, (int)y1, (int)BoxWidth, (int)BoxHeight, 1, (entity.Team == LocalPlayer.Team) ? Color.Blue : Color.Red);
                            Overlay.Line(Overlay.Width / 2, Overlay.Height, (int)OriginPosition2D.X, (int)OriginPosition2D.Y, (entity.Team == LocalPlayer.Team) ? Color.Blue : Color.Red);
                            Overlay.Bar((int)(x1 + BoxWidth + 10), (int)HeadPosition2D.Y, (int)(BoxWidth / 8), (int)(BoxHeight), 1, Color.FromArgb(2, 2, 2), entity.Health, 100, Color.LimeGreen);
                        }
                    }
                }
                Overlay.Text("Overlay FPS : " + Overlay.RenderFPS, 10, 30, Color.White);
                Overlay.Render();
            }
        }

        public static void OverlayUpdate()
        {
            while(true)
            {
                if (Utils.ForegroundProcess().ProcessName == "csgo")
                {
                    Overlay.Visible = true;
                    Overlay.TopMost = true;

                    LFOverlay.Classes.Variables.Structs.RECT rect;
                    if (WinAPI.GetWindowRect(Utils.ForegroundProcess().MainWindowHandle, out rect))
                    {
                        Overlay.Width = rect.right - rect.left;
                        Overlay.Height = rect.bottom - rect.top;
                        Overlay.X = rect.left;
                        Overlay.Y = rect.top;
                    }
                }
                else
                {
                    Overlay.Visible = false;
                    Overlay.TopMost = false;
                }
                Utils.FlushMemory();
                Thread.Sleep(1500);
            }
        }
    }
}
