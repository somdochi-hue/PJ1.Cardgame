using System;
using System.Collections.Specialized;
using System.Threading;
using System.Xml.Schema;

int playCount = 0;
int completCount = 0;
int[,] box = new int[4, 4];
bool[,] star = new bool[4, 4];

Shuffle(box);

while (playCount < 20 && completCount < 8)
{
    Console.Clear();
    Console.WriteLine("===카드 짝 맞추기 게임===");
    Console.WriteLine();
    Console.WriteLine(" \t1열\t2열\t3열\t4열");

    for (int row = 0; row < 4; row++)
    {
        Console.Write($"{row + 1}행\t");
        for (int col = 0; col < 4; col++)
        {
            if (star[row, col])
            {
                Console.Write($"[ {box[row, col]}]\t");
            }
            else
            {
                Console.Write("**\t");
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine($"시도 횟수: {playCount}/20 | 찾은 쌍: {completCount}/8");

    Console.WriteLine();

    Console.Write("첫 번째 카드 행 입력: ");
    int row1 = int.Parse(Console.ReadLine()) - 1;
    Console.Write("첫 번째 카드 열 입력: ");
    int col1 = int.Parse(Console.ReadLine()) - 1;

    Console.WriteLine();

    if (star[row1, col1])
    {
        Console.WriteLine("이미 오픈된 카드입니다. 다시 선택하세요.");
        Console.ReadKey();
        continue;
    }
    star[row1, col1] = true;
    Console.Clear();
    PrintBoard(box, star);

    Console.Write("두 번째 카드 행 입력: ");
    int row2 = int.Parse(Console.ReadLine()) - 1;
    Console.Write("두 번째 카드 열 입력: ");
    int col2 = int.Parse(Console.ReadLine()) - 1;

    if ((row1 == row2 && col1 == col2) || star[row2, col2])
    {
        Console.WriteLine("잘못된 선택입니다.");
        star[row1, col1] = false;
        Console.ReadKey();
        continue;
    }

    star[row2, col2] = true;
    playCount++;
    Console.Clear();
    PrintBoard(box, star);

    if (box[row1, col1] == box[row2, col2])
    {
        Console.WriteLine("짝을 맞추셨습니다!");
        completCount++;
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("틀렸습니다! 카드를 다시 덮습니다.");
        Console.ReadKey();
        star[row1, col1] = false;
        star[row2, col2] = false;
    }
}

Console.Clear();
if (completCount == 8)
{
    Console.WriteLine("축하합니다! 모든 짝을 맞추셨습니다!");
}
else
{
    Console.WriteLine("시도 횟수(20회)를 모두 소모하여 게임 오버되었습니다.");
}

void Shuffle(int[,] box)
{
    int[] numberCount = new int[9];
    bool[] isValue = new bool[16];
    int count = 0;
    Random random = new Random();

    while (true)
    {
        int number = random.Next(1, 9);   // 1~8
        int index = random.Next(0, 16);   // 0~15
        if (isValue[index])
            continue;
        if (numberCount[number] >= 2)
            continue;
        isValue[index] = true;
        numberCount[number]++;
        count++;

        box[index / 4, index % 4] = number;

        if (count == 16)
            break;
    }

}
void PrintBoard(int[,] box, bool[,] star)
{
    Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
    Console.WriteLine();
    Console.WriteLine(" \t1열\t2열\t3열\t4열");
    for (int row = 0; row < 4; row++)
    {
        Console.Write($"{row + 1}행\t");
        for (int col = 0; col < 4; col++)
        {
            if (star[row, col])
            {
                Console.Write($"[ {box[row, col]} ]\t");
            }
            else
            {
                Console.Write("**\t");
            }
        }
        Console.WriteLine();
    }
}