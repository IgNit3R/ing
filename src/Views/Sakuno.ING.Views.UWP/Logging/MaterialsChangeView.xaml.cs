﻿using Sakuno.ING.Game.Logger.Entities;
using Sakuno.ING.Shell;
using Sakuno.ING.ViewModels.Logging;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Sakuno.ING.Views.UWP.Logging
{
    [ExportView("MaterialsChangeLogs")]
    public sealed partial class MaterialsChangeView : UserControl
    {
        private readonly MaterialsChangeLogsVM ViewModel;
        public MaterialsChangeView(MaterialsChangeLogsVM viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();

            Loaded += (_, __) => comboBox.SelectedIndex = 2;
        }

        public static Geometry ConvertShape(MaterialsChartData chartData, int id)
        {
            if (chartData is null) return null;

            var duration = chartData.End - chartData.Start;
            Point SelectPoint(MaterialsChangeEntity p)
                => new Point((p.TimeStamp - chartData.Start) / duration,
                    id switch
                    {
                        0 => p.Materials.Fuel / 300000.0,
                        1 => p.Materials.Bullet / 300000.0,
                        2 => p.Materials.Steel / 300000.0,
                        3 => p.Materials.Bauxite / 300000.0,
                        4 => p.Materials.InstantBuild / 3000.0,
                        5 => p.Materials.InstantRepair / 3000.0,
                        6 => p.Materials.Development / 3000.0,
                        7 => p.Materials.Improvement / 3000.0,
                        _ => 0
                    });

            var figure = new PathFigure
            {
                IsClosed = false,
                StartPoint = SelectPoint(chartData.Entities[0])
            };

            foreach (var p in chartData.Entities)
                figure.Segments.Add(new LineSegment
                {
                    Point = SelectPoint(p)
                });

            return new PathGeometry
            {
                Figures =
                {
                    new PathFigure { Segments = { new LineSegment() } },
                    figure,
                    new PathFigure { StartPoint = new Point(1, 1), Segments = { new LineSegment { Point = new Point(1, 1) } } }
                }
            };
        }
    }
}
