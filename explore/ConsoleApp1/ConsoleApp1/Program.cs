namespace TestApp;

using System;

public class Task
{
    public static void Main(string[] args)
    {
        var root = new Node()
        {
            Name = "root",
            Children = new[]
            {
                new Node()
                {
                    Name = "A0",
                    Value = 0,
                    Children = new[]
                    {
                        new Node()
                        {
                            Name = "A01",
                            Value = 0,
                            Children = new[]
                            {
                                new Node()
                                {
                                    Name = "A001",
                                    Value = 4,
                                },
                                new Node()
                                {
                                    Name = "A002",
                                    Value = 8,
                                },
                                new Node()
                                {
                                    Name = "A003",
                                    Value = 2,
                                }
                            }
                        },
                        new Node()
                        {
                            Name = "A002",
                            Value = 0,
                            Children = new[]
                            {
                                new Node()
                                {
                                    Name = "A0001",
                                    Value = 3,
                                },
                                new Node()
                                {
                                    Name = "A0002",
                                    Value = 5,
                                    Children = new[]
                                    {
                                        new Node()
                                        {
                                            Name = "A00032",
                                            Value = 4
                                        }
                                    }
                                },
                                new Node()
                                {
                                    Name = "A0003",
                                    Value = 0,
                                    Children = new[]
                                    {
                                        new Node()
                                        {
                                            Name = "A00031",
                                            Value = 4
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Node()
                {
                    Name = "B0",
                    Value = 0,
                    Children = new[]
                    {
                        new Node()
                        {
                            Name = "B01",
                            Value = 0,
                            Children = new[]
                            {
                                new Node()
                                {
                                    Name = "B001",
                                    Value = 1,
                                },
                                new Node()
                                {
                                    Name = "B002",
                                    Value = 2,
                                },
                            }
                        }
                    }
                },
            }
        };

        Walkthrough(root);
        Console.WriteLine(root.Value);
        Console.WriteLine(root.Children.ToList()[0].Value);
        Console.WriteLine(root.Children.ToList()[0].Children.ToList()[0].Value);
        Console.WriteLine(root.Children.ToList()[0].Children.ToList()[1].Value);
        Console.WriteLine(root.Children.ToList()[1].Value);
        Console.WriteLine(root.Children.ToList()[1].Children.ToList()[0].Value);
        Console.WriteLine(MaxPath(root));
    }

    /// <summary>
    /// Проходит по дереву <see cref="Node"/> заполняя Value для каждого узла,
    /// у которого есть хотя бы один предок. Value - это сумма всех значений Value у дочерних узлов.
    /// </summary>
    private static int Walkthrough(Node node)
    {
        if (node.Children == null)
            return node.Value;

        var sum = node.Children.Sum(Walkthrough);
        node.Value = sum;

        return sum;
    }

    /// <summary>
    /// Проходит по дереву <see cref="Node"/> и строит путь до самого глубокого потомка.
    /// Например: A0->A01->A002->A0003
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static string MaxPath(Node node)
    {
        // выбираем путь до самого глубокого потомка по количеству вхождений подстроки "->" :)
        return GetAllPaths(node).MaxBy(path => path.Split("->").Length) ?? string.Empty;
    }

    /// <summary>
    /// Возвращает итератор по всем возможным вариантам путей от корня до узлов, не имеющих потомков.
    /// Например: [A0->A01->A001, A0->A01->A002, ...]
    /// </summary>
    /// <param name="node">Текущий узел дерева</param>
    /// <param name="path">Путь для текущего узла дерева</param>
    /// <returns></returns>
    private static IEnumerable<string> GetAllPaths(Node node, string? path = null)
    {
        // если потомков нет, то возвращаем путь к узлу и выходим из рекурсии
        if (node.Children == null)
        {
            yield return path + node.Name;
            yield break;
        }

        // формируем путь, исключая корневой узел
        path = path != null ? path + $"{node.Name}->" : string.Empty;

        // получаем потомков у текущего узла и рекурсивно формируем пути для каждого из них
        foreach (var childPath in node.Children.SelectMany(child => GetAllPaths(child, path)))
        {
            yield return childPath;
        }
    }
}

public class Node
{
    public string Name { get; set; }

    public int Value { get; set; }

    public IEnumerable<Node> Children { get; set; }
}