using System;

class Program
{
    static void Main()
    {
        const int CardCount = 16;
        const int ColumCount = 4;

        int[] cards = CreateCards(CardCount);
        bool[] matched = new bool[CardCount];
        Random random = new Random();
        ShuffleCards(cards, random);

        int attemps = 0;
        int matchedParis = 0;

        while (matchedParis < CardCount / 2)
        {
            DisplayBoard(cards, matched, ColumCount, attemps, matchedParis);
            int firstIndex = ReadCardIndex("첫 번째", matched, ColumCount, -1);
            DisplayBoard(cards, matched, ColumCount, attemps, matchedParis, firstIndex);
            int secondIndex = ReadCardIndex("두 번째", matched, ColumCount, firstIndex);
            DisplayBoard(cards, matched, ColumCount, attemps, matchedParis, firstIndex, secondIndex);

            attemps++;
            bool isMatch = cards[firstIndex] == cards[secondIndex];
            if (isMatch)
            {
                matched[firstIndex] = matched[secondIndex] = true;
                matchedParis++;
            }
            DisplayBoard(cards, matched, ColumCount, attemps, matchedParis, firstIndex, secondIndex);
            Console.WriteLine(isMatch ? "매칭 성공" : "매칭 실패");

            WaitForEnter();
        }
        Console.WriteLine($"게임 클리어! 총 시도 횟수: {attemps}");
        WaitForEnter();
    }

    static int ReadCardIndex(string choiceName, bool[] matched, int columnCount, int firstIndex)
    {
        while (true)
        {
            Console.Write($"{choiceName} 카드 위치 선택 (x, y): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(',');
            int i = 0;
            int j = 0;
            if (parts.Length == 2 &&
                !int.TryParse(parts[0], out i) &&
                !int.TryParse(parts[1], out j))
            {
                Console.WriteLine("[행, 열] 형식으로 숫자를 입력하세요");
                continue;
            }

            int rowCount = matched.Length / columnCount;
            if (i < 0 || i >= rowCount ||
                j < 0 || j >= columnCount)
            {
                Console.WriteLine("맞는 범위 입력해라");
            }

            int index = i * columnCount + j;

            if (index < 0 || index >= matched.Length)
            {
                Console.WriteLine($"0~{matched.Length - 1} 까지 입력하세요");
                continue;
            }

            if (index == firstIndex)
            {
                Console.WriteLine("같은 카드를 두 번 선택할 수 없습니다.");
                continue;
            }

            return index;
        }
    }

    static void WaitForEnter()
    {
        Console.WriteLine("확인했으면 Enter를 누르세요.");
        Console.ReadLine();
    }

    static void DisplayBoard(int[] cards, bool[] matched, int columnCount,
        int attemps, int matchedPairs, int firstIndex = -1, int secondIndex = -1)
    {
        Console.Clear();

        Console.WriteLine("=== 카드 매칭 게임 ===");
        Console.WriteLine($"시도 횟수: {attemps}, 찾은 쌍: {matchedPairs}");
        Console.WriteLine();

        Console.WriteLine("카드 위치");
        for (int i = 0; i < cards.Length; i++)
        {
            Console.Write($"{i,4}");
            if ((i + 1) % columnCount == 0)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("현재 보드");
        for (int i = 0; i < cards.Length; i++)
        {
            bool isVisible = matched[i] || firstIndex == i || secondIndex == i;
            string face = isVisible ? cards[i].ToString() : "*";
            Console.Write($"{face,4}");

            if ((i + 1) % columnCount == 0)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
    }


    static int[] CreateCards(int cardCount)
    {
        int[] cards = new int[cardCount];
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = i / 2 + 1; // 1, 1, 2, 2, 3, 3 ..
        }
        return cards;
    }

    static void ShuffleCards(int[] cards, Random random)
    {
        for (int i = cards.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            int temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

}