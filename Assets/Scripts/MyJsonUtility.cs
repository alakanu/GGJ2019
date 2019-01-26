using System.Collections.Generic;
using System.IO;
using UnityEngine;

static class MyJsonUtility
{
    const string PATH = "/JsonFiles/Dialogues.json";
    public static Dictionary<string, Dialogue> LoadDialogues()
    {
        Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();

        string jsonFileContent = File.ReadAllText(Application.dataPath + PATH).Replace("\t", "").Replace("\n", "").Replace("\r", "");
        int currentPosition = 1;
        string key, jsonEntry;

        while (TryGetNextEntry(jsonFileContent, ref currentPosition, out key, out jsonEntry))
        {
            DialogueJson dialogueRaw = JsonUtility.FromJson<DialogueJson>(jsonEntry);
            Dialogue dialogue;
            if (dialogues.TryGetValue(key, out dialogue) == false)
            {
                dialogue = new Dialogue();
                dialogues.Add(key, dialogue);
            }

            dialogue.DialogueText = dialogueRaw.DialogueText;
            AnswerJson[] answersRaw = dialogueRaw.Answers;
            Answer[] dialogueAnswers = new Answer[answersRaw.Length];
            for (int i = 0; i < answersRaw.Length; i++)
            {
                var answerRaw = answersRaw[i];
                var answer = new Answer();
                answer.AnswerText = answerRaw.AnswerText;
                string nextDialogueKey = answerRaw.NextDialogue;
                Dialogue nextDialogue;

                if (dialogues.TryGetValue(nextDialogueKey, out nextDialogue) == false)
                {
                    nextDialogue = new Dialogue();
                    dialogues.Add(nextDialogueKey, nextDialogue);
                }

                answer.NextDialogue = nextDialogue;
                dialogueAnswers[i] = answer;
            }
            dialogue.Answers = dialogueAnswers;
        }


        return dialogues;
    }

    static bool TryGetNextEntry(
        string jsonFileContent,
        ref int currentPosition,
        out string key,
        out string jsonEntry)
    {
        key = null;
        jsonEntry = null;

        if (TrySeekUntilNextEntry(jsonFileContent, ref currentPosition))
        {
            key = GetKey(jsonFileContent, ref currentPosition);

            int openBracesCount = 0;
            int start = currentPosition;
            int length;
            bool seeking = true;

            for (length = 0; seeking; length++)
            {
                switch (jsonFileContent[start + length])
                {
                    case '{':
                        ++openBracesCount;
                        break;
                    case '}':
                        seeking = --openBracesCount > 0;
                        break;
                }
            }

            currentPosition = start + length;
            jsonEntry = jsonFileContent.Substring(start, length);
            return true;
        }
        return false;

    }

    static string GetKey(string jsonFileContent, ref int currentPosition)
    {
        int length = 1;
        while (jsonFileContent[currentPosition + length] != '"')
        {
            length++;
        }

        string result = jsonFileContent.Substring(currentPosition + 1, length - 1);

        currentPosition += length + 2;// the " plus the :
        return result;
    }

    static bool TrySeekUntilNextEntry(string jsonFileContent, ref int currentPosition)
    {
        while (currentPosition < jsonFileContent.Length - 1)
        {
            if (jsonFileContent[currentPosition] == '"')
            {
                return true;
            }
            currentPosition++;
        }

        return false;
    }


}

