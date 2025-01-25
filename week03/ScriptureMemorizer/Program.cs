using System;
class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ')
                     .Select(word => new Word(word))
                     .ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        int hiddenCount = 0;

        while (hiddenCount < numberToHide)
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hiddenCount++;
            }
        }
    }

    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n{wordsText}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }

    public List<Word> Words => _words; // I included property to expose words
}


class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_verse == _endVerse)
            return $"{_book} {_chapter}:{_verse}";
        else
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show() // I added to reveal hidden words
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Doctrine and Covenant", 2, 1, 3);
        string verseText = "\nBehold, I will reveal unto you the Priesthood, by the hand of Elijah the prophet, before the coming of the great and dreadful day of the Lord. " +
                           "And he shall plant in the hearts of the children the promises made to the fathers, and the hearts of the children shall turn to their fathers. " +
                           "If it were not so, the whole earth would be utterly wasted at his coming.";

        Scripture scripture = new Scripture(reference, verseText);

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nType 'hint' to reveal a hidden word, 'quit' to exit, or press Enter to continue:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                Console.WriteLine("Well done you did it!");
                break;
            }
            else if (input.ToLower() == "hint")
            {
                var hiddenWord = scripture.Words.FirstOrDefault(word => word.IsHidden());
                if (hiddenWord != null)
                {
                    hiddenWord.Show();
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine($"\nHint revealed: {hiddenWord.GetDisplayText()}");
                }
                else
                {
                    Console.WriteLine("\nAll words are already revealed!");
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else
            {
                scripture.HideRandomWords(3);
            }
        }
    }
}