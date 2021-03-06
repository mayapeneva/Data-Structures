﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        ITextEditor editor = new TextEditor();
        var users = new Dictionary<string, bool>();

        var regex = new Regex("\"(.*)\"");
        string input;
        while ((input = Console.ReadLine()) != "end")
        {
            var command = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string userName;
            switch (command[0])
            {
                case "login":
                    userName = command[1];
                    users[userName] = true;
                    editor.Login(userName);
                    break;

                case "logout":
                    userName = command[1];
                    users[userName] = false;
                    editor.Logout(userName);
                    break;

                case "users":
                    IEnumerable<string> result = new List<string>();
                    result = editor.Users(command.Length > 1 ? command[1] : "");

                    foreach (var user in result)
                    {
                        if (users[user])
                        {
                            Console.WriteLine(user);
                        }
                    }

                    break;

                default:
                    userName = command[0];
                    if (!users.ContainsKey(userName) || users[userName] == false)
                    {
                        continue;
                    }

                    var match = regex.Match(input);
                    var text = match.Groups[1].Value;
                    var comm = command[1];
                    switch (comm)
                    {
                        case "insert":
                            editor.Insert(userName, int.Parse(command[2]), text);
                            break;

                        case "prepend":
                            editor.Prepend(userName, text);
                            break;

                        case "substring":
                            editor.Substring(userName, int.Parse(command[2]), int.Parse(command[3]));
                            break;

                        case "delete":
                            editor.Delete(userName, int.Parse(command[2]), int.Parse(command[3]));
                            break;

                        case "clear":
                            editor.Clear(userName);
                            break;

                        case "length":
                            Console.WriteLine(editor.Length(userName));
                            break;

                        case "print":
                            Console.WriteLine(editor.Print(userName));
                            break;

                        case "undo":
                            editor.Undo(userName);
                            break;
                    }

                    break;
            }
        }
    }
}