using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnglishData")]
public class EnglishData : LanguageData
{
    private Dictionary<string, string> _menuTexts = new Dictionary<string, string>()
    {
        {"title_mainMenu", "Crosswords 2.0"},
        {"confirm_button", "Confirm"},
        {"back_button", "Back"},
        {"start_button", "Start"},
        {"select_language", "Language"},
        {"veritcalWordHint", "Verctical word: "},
        {"horizontalWordHint", "Horizontal word: "}
    };


    public override Dictionary<string, string> MenuTexts => _menuTexts;


    private Dictionary<string, (string word, string hint)> _dialogueTexts = new Dictionary<string, (string word, string hint)>()
    {
        {"w1", ("apple", "A fruit that keeps doctors away")},
    {"w2", ("router", "Device that distributes internet like crumbs to pigeons")},
    {"w3", ("particle", "Tiny piece of matter or dust in your eye")},
    {"w4", ("bridge", "Connects two lands or breaks under trolls")},
    {"w5", ("castle", "Home of a medieval mortgage problem")},
    {"w6", ("wizard", "Old man who shouts 'fireball!'")},
    {"w7", ("puzzle", "What you're currently solving")},
    {"w8", ("window", "Transparent wall you clean twice a year")},
    {"w9", ("forest", "Where trees go to gossip")},
    {"w10", ("shadow", "Follows you everywhere, rent-free")},
    {"w11", ("clock", "Annoying device reminding you of time")},
    {"w12", ("coffee", "Liquid motivation")},
    {"w13", ("planet", "Big rock in space doing laps around a star")},
    {"w14", ("mirror", "Honest but cruel friend")},
    {"w15", ("storm", "When the sky gets emotional")},
    {"w16", ("river", "Nature’s moving mirror")},
    {"w17", ("castle", "A house with delusions of grandeur")},
    {"w18", ("helmet", "Head protection for those with plans")},
    {"w19", ("engine", "Heart of a car, or reason it won’t start")},
    {"w20", ("camera", "Catches your soul, or just bad angles")},
    {"w21", ("pirate", "Thief with style and a parrot")},
    {"w22", ("anchor", "Stops a ship, or a metaphorical burden")},
    {"w23", ("dragon", "Mythical fire hazard")},
    {"w24", ("bottle", "Liquid prison")},
    {"w25", ("market", "Place where your money disappears")},
    {"w26", ("garden", "Outdoor struggle against weeds")},
    {"w27", ("planet", "Big cosmic marble")},
    {"w28", ("cloud", "Fluffy sky data storage")},
    {"w29", ("knight", "Metal-clad guy on horseback")},
    {"w30", ("forest", "Where elves and mosquitoes coexist")},
    {"w31", ("lantern", "Old-school flashlight")},
    {"w32", ("train", "Long metal snake on rails")},
    {"w33", ("harbor", "Parking lot for ships")},
    {"w34", ("desert", "Endless sandbox")},
    {"w35", ("compass", "Always points north, even when you’re lost")},
    {"w36", ("scroll", "Old-fashioned PDF")},
    {"w37", ("sword", "Argument settler from medieval times")},
    {"w38", ("storm", "Sky’s anger management issue")},
    {"w39", ("castle", "Fortress for kings and bad acoustics")},
    {"w40", ("tavern", "Medieval Starbucks")},
    {"w41", ("armor", "Second skin for people with trust issues")},
    {"w42", ("whisper", "Secret delivered on low volume")},
    {"w43", ("memory", "Thing you lose right after learning")},
    {"w44", ("signal", "What your phone never has")},
    {"w45", ("portal", "Shortcut with side effects")},
    {"w46", ("robot", "Servant who will remember this later")},
    {"w47", ("galaxy", "Massive star neighborhood")},
    {"w48", ("library", "Fortress of quiet knowledge")},
    {"w49", ("engineer", "Person who fixes problems you didn’t know existed")},
    {"w50", ("crystal", "Pretty rock with good vibes")},
    };


    public override Dictionary<string, (string word, string hint)> DialogueTexts => _dialogueTexts;


    private char[] _alphabet =
    {
        'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',

        'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',

        'z', 'x', 'c', 'v', 'b', 'n', 'm'
    };


    public override char[] KeyboardAlphabet => _alphabet;

    

    private int[] _keyboardRows = 
    {
        10, 9, 7
    };

    public override int[] KeyboardRowLengths => _keyboardRows;
}
