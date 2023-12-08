using System;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace ClassLibrary1
{
    public class Game : Control
    {
        protected Color _ObjColor;
        protected int[,] _gameData;
        protected int[,,] _paintData;
        protected int result;
        protected int _Flag;
        protected int _ObjSize;
        protected int _curX;
        protected int _curY;
        protected int _deltaX;
        protected int _deltaY;
        Random rnd = new Random();

        public Game() : base()
        {
            _gameData = new int[12, 12];
            _paintData = new int[3, 5, 5];
            RandFigure(0);
            RandFigure(1);
            RandFigure(2);
            _ObjColor = Color.Gray; // обводка 
            _Flag = -1; // разрешен ли сдвиг фигуры которую выбрал пользователь 
            _curY = 0;
            _curX = 0;
            _deltaY = 0;
            _deltaX = 0;
            result = 0;
        }

        protected override void OnMouseDown(MouseEventArgs e) // если поймали одну из областей фигур
        {
            base.OnMouseDown(e);
            if (((e.Location.X >= ObjSize * 14)
                && (e.Location.X <= ObjSize * 19))
                && (e.Location.Y >= ObjSize * 1)
                && (e.Location.Y <= ObjSize * 6))
            {
                _Flag = 0;
                _curX = e.Location.X;
                _curY = e.Location.Y;
                _deltaX = e.Location.X - ObjSize * 14;
                _deltaY = e.Location.Y - ObjSize * 1;
            }

            if (((e.Location.X >= ObjSize * 20)
            && (e.Location.Y >= ObjSize * 1)
            && (e.Location.X <= ObjSize * 25))
            && (e.Location.Y <= ObjSize * 6))
            {
                _Flag = 1;
                _curX = e.Location.X;
                _curY = e.Location.Y;
                _deltaX = e.Location.X - ObjSize * 20;
                _deltaY = e.Location.Y - ObjSize * 1;
            }

            if (((e.Location.X >= ObjSize * 14)
                && (e.Location.Y >= ObjSize * 7)
                && (e.Location.X <= ObjSize * 19))
                && (e.Location.Y <= ObjSize * 12))
            {
                _Flag = 2;
                _curX = e.Location.X;
                _curY = e.Location.Y;
                _deltaX = e.Location.X - ObjSize * 14;
                _deltaY = e.Location.Y - ObjSize * 7;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) // запоманание позиции при движении для отрисовки
        {
            base.OnMouseMove(e);
            if (_Flag >= 0)
            {
                _curX = e.Location.X;
                _curY = e.Location.Y;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) // проверка и конечная позиция фигуры в случае пустоты на установке
        {
            base.OnMouseUp(e);
            if ((_curX >= _ObjSize)
                && (_curX <= _ObjSize * 13)
                && (_curY >= _ObjSize)
                && (_curY <= _ObjSize * 13)
                && _Flag > -1)
            {
                _curX = _curX - ObjSize;
                _curY = _curY - ObjSize;
                MoveData(_Flag);
            }
            Invalidate();
            _Flag = -1;
        }

        protected void MoveData(int index)
        {
            if (CheckMoveData(index) == true)
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (_paintData[index, row, col] > 0)
                        {
                            _gameData[row + (_curY - _deltaY) / (_ObjSize), col + (_curX - _deltaX) / (_ObjSize)] = _paintData[index, row, col];
                            _paintData[index, row, col] = 0;
                        }
                    }
                }
                SearchLineHorizontalData();
                SearchLineVertData();
                if ((CreatingShapesIfNone(0) == true) && (CreatingShapesIfNone(1) == true) && (CreatingShapesIfNone(2) == true))
                {
                    RandFigure(0);
                    RandFigure(1);
                    RandFigure(2);
                }
                CantFit();
            }
            Invalidate();
        } // перемещение фигуры из массива _paintData в _gameData




        protected void SearchLineHorizontalData() // проверка и удаление горизонтальной линии 
        {
            int cnth = 0;
            for (int row = 0; row < 12; row++)
            {
                for (int col = 0; col < 12; col++)
                {
                    if (_gameData[row, col] > 0)
                    {
                        cnth++;
                    }
                }
                if (cnth == 12)
                {
                    SearchLineVertData();
                    for (int col = 0; col < 12; col++)
                    {
                        _gameData[row, col] = 0;

                    }
                    cnth = 0;
                    result = result+12;
                    Invalidate();
                }
                else
                {
                    cnth = 0;
                }
            }
        }

        protected void SearchLineVertData()
        {
            int cntv = 0;
            for (int col = 0; col < 12; col++)
            {
                for (int row = 0; row < 12; row++)
                {
                    if (_gameData[row, col] > 0)
                    {
                        cntv++;
                    }
                }
                if (cntv == 12)
                {
                    SearchLineHorizontalData();
                    for (int row = 0; row < 12; row++)
                    {
                        _gameData[row, col] = 0;

                    }
                    cntv = 0;
                    result = result + 12;
                    Invalidate();
                }
                else
                {
                    cntv = 0;
                }
            }
        } //проверка и удаление вертикальной линии

        protected bool CreatingShapesIfNone(int index)
        {
            int cnt = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (_paintData[index, row, col] == 0)
                    {
                        cnt++;
                    }
                }
            }
            if (cnt == 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        } // Создание рандомных фигур если их установили

        protected bool CheckMoveData(int index)
        {
            try
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (_paintData[index, row, col] > 0 && _gameData[row + (_curY - _deltaY) / (_ObjSize), col + (_curX - _deltaX) / (_ObjSize)] > 0)
                        {
                            return false;
                        }

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            

        } // Проверка игрового поля на установку фигуры в область


        public int ObjSize // Объявление размера 
        {

            get
            {
                return _ObjSize;
            }
            set
            {

                if (value < 20)
                {
                    value = 20;
                }
                else
                {
                    if (value > 25)
                    {
                        value = 25;
                    }
                }

                if (value != _ObjSize)
                {
                    _ObjSize = value;
                    Invalidate();
                }

            }
        }
        public Color ObjColor// Объявление 
        {

            get
            {
                return _ObjColor;
            }
            set
            {
                _ObjColor = value;
                Invalidate();
            }
        }

        protected int[,] ObjectData(int index)
        {
            int[,] objectData = new int[5, 5];
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    objectData[row, col] = _paintData[index, row, col];
                }
            }

            return objectData;
        } // Преобразование из 3х мерного массива в 2х мерный

        protected override void OnPaint(PaintEventArgs e) // метод для отрисовки массива
        {
            base.OnPaint(e);
            Brush B = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(B, ClientRectangle);

            DrawRect(e, ObjSize, ObjSize, 12, 12, Color.Gray, _ObjColor, _gameData);
            DrawRect(e, 14 * _ObjSize, 1 * _ObjSize, 5, 5, Color.Gray, _ObjColor, ObjectData(0));
            DrawRect(e, 20 * _ObjSize, 1 * _ObjSize, 5, 5, Color.Gray, _ObjColor, ObjectData(1));
            DrawRect(e, 14 * _ObjSize, 7 * _ObjSize, 5, 5, Color.Gray, _ObjColor, ObjectData(2));
            switch (_Flag)
            {
                case 0:
                    DrawRect(e, 14 * _ObjSize, 1 * _ObjSize, 5, 5, Color.Gray, _ObjColor, null);
                    DrawDynamic(e, _curX - _deltaX, _curY - _deltaY, 5, 5, _ObjColor, ObjectData(0));// отрисовываем фигуру в тек положение 
                    break;
                case 1:
                    DrawRect(e, 20 * _ObjSize, 1 * _ObjSize, 5, 5, Color.Gray, _ObjColor, null);
                    DrawDynamic(e, _curX - _deltaX, _curY - _deltaY, 5, 5, _ObjColor, ObjectData(1));
                    break;
                case 2:
                    DrawRect(e, 14 * _ObjSize, 7 * _ObjSize, 5, 5, Color.Gray, _ObjColor, null);
                    DrawDynamic(e, _curX - _deltaX, _curY - _deltaY, 5, 5, _ObjColor, ObjectData(2));
                    break;
            }
            string numberText = "Результат: "+result.ToString();
            Font numberFont = new Font("Arial", 12); 
            Brush numberBrush = new SolidBrush(Color.Black); 
            e.Graphics.DrawString(numberText, numberFont, numberBrush, 20 * _ObjSize, 9 * _ObjSize);
        }

        private void DrawRect(PaintEventArgs e, int X, int Y, int RowSpan, int ColSpan,
            Color Background, Color Foreground, int[,] Figure)
        {
            Brush F1 = new SolidBrush(Background);
            for (int row = 0; row < RowSpan; row++)
            {
                for (int col = 0; col < ColSpan; col++)
                {
                    Brush F;
                    if (Figure != null && Figure[row, col] > 0)
                    {
                        F = new SolidBrush(CheckColor(Foreground, Figure, row, col));
                    }
                    else
                    {
                        F = F1;
                    }
                    e.Graphics.FillRectangle(F, X + _ObjSize * col, Y + _ObjSize * row, _ObjSize - 3, _ObjSize - 3);
                }
            }
        } // Создание фигуры

        private void DrawDynamic(PaintEventArgs e, int X, int Y, int RowSpan, int ColSpan, Color Foreground, int[,] Figure)
        {

            for (int row = 0; row < RowSpan; row++)
            {
                for (int col = 0; col < ColSpan; col++)
                {
                    Brush F;
                    if (Figure[row, col] > 0)
                    {
                        F = new SolidBrush(CheckColor(Foreground, Figure, row, col));
                        e.Graphics.FillRectangle(F, X + _ObjSize * col, Y + _ObjSize * row, _ObjSize - 3, _ObjSize - 3);
                    }
                }
            }
        }

        protected void RandFigure(int index)
        {

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    _paintData[index, row, col] = 0;
                }
            }
            int Color = rnd.Next(1, 6);
            int Figure = rnd.Next(1, 10);


            switch (Figure)
            {
                case 1:
                    _paintData[index, 2, 2] = Color;
                    break;
                case 2:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    break;
                case 3:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 2, 3] = Color;
                    break;
                case 4:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 1, 2] = Color;
                    break;
                case 5:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 1, 2] = Color;
                    break;
                case 6:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 1, 2] = Color;
                    _paintData[index, 3, 2] = Color;
                    break;
                case 7:
                    _paintData[index, 2, 0] = Color;
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 2, 4] = Color;
                    break;
                case 8:
                    _paintData[index, 0, 2] = Color;
                    _paintData[index, 1, 2] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 3, 2] = Color;
                    _paintData[index, 4, 2] = Color;
                    break;
                case 9:
                    _paintData[index, 2, 1] = Color;
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    _paintData[index, 1, 1] = Color;
                    _paintData[index, 1, 2] = Color;
                    _paintData[index, 1, 3] = Color;
                    _paintData[index, 3, 1] = Color;
                    _paintData[index, 3, 2] = Color;
                    _paintData[index, 3, 3] = Color;
                    break;
                case 10:
                    _paintData[index, 2, 2] = Color;
                    _paintData[index, 2, 3] = Color;
                    break;
            }
            Invalidate();
        } // Формирование рандомной фигуры любого цвета

        protected Color CheckColor(Color Foreground, int[,] Figure, int row, int col)
        {

            switch (Figure[row, col])
            {
                case 1:
                    Foreground = Color.Red;
                    break;
                case 2:
                    Foreground = Color.Yellow;
                    break;
                case 3:
                    Foreground = Color.Green;
                    break;
                case 4:
                    Foreground = Color.Blue;
                    break;
                case 5:
                    Foreground = Color.Orange;
                    break;
                case 6:
                    Foreground = Color.Pink;
                    break;
            }
            return Foreground;
        }  // Проеврка цвета для изменения в методе onPaint

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams P = base.CreateParams;
                P.ExStyle = P.ExStyle | 0x02000000;
                return P;
            }
        } // Для корректного отображение отрисовки



        protected void CantFit()
        {
            if (CanFitFigure(ObjectData(0), _gameData) == false
                && CanFitFigure(ObjectData(1), _gameData) == false
                && CanFitFigure(ObjectData(2), _gameData) == false)
            {
                MessageBox.Show("Игра окончена!");
                result = 0;
                RandFigure(0);
                RandFigure(1);
                RandFigure(2);

                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        _gameData[i, j] = 0;
                    }
                }
            }
        }


        protected bool CanFitFigure(int[,] smallerArray, int[,] largerArray)
        {
            int smallerWidth = smallerArray.GetLength(1);
            int smallerHeight = smallerArray.GetLength(0);
            int largerWidth = largerArray.GetLength(1);
            int largerHeight = largerArray.GetLength(0);

            for (int i = 0; i <= largerHeight - smallerHeight; i++)
            {
                for (int j = 0; j <= largerWidth - smallerWidth; j++)
                {
                    bool canFit = true;

                    for (int k = 0; k < smallerHeight; k++)
                    {
                        for (int l = 0; l < smallerWidth; l++)
                        {
                            if (smallerArray[k, l] != 0 && largerArray[i + k, j + l] != 0)
                            {
                                canFit = false;
                                break;
                            }
                        }

                        if (!canFit)
                        {
                            break;
                        }
                    }

                    if (canFit)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
             int minWidth = _ObjSize*20;  // Минимальная ширина, замените на свое значение
             int minHeight = _ObjSize * 7; // Минимальная высота, замените на свое значение

            // Ограничение минимальных размеров элемента управления
            width = Math.Max(width, minWidth);
            height = Math.Max(height, minHeight);

            base.SetBoundsCore(x, y, width, height, specified);
        }
    }
}