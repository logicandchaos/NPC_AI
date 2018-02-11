using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Ai for Advanced NPC Communication and Relation
/// This is an ongoing project for improving interactions with Non Player Characters in electronic games giving them artificial
/// emotions, personality type and creating relationships.
/// 
/// This is a program ti simulate emotions for NPC characters, the temperment value is just temperary and will be replaced
/// once the personality trait model is complete, this is a work in progress, the final version will have 3 layers, emotional,
/// personanlity and relationship.
/// 
/// I am attempting to do this by using 2D coordinates for everything, the three different models I will be using are Valence/Arousal
/// for emotions of the characters, the most dynamic, the model of Love/Trust the 2 cornerstones for any relationship, less dynamic 
/// than emotions, but still changes, the final thing is personality I found Extraversion–Introversion/Stability model for that, this
/// will be static, to keep them in character.
/// 
/// Valence/Arousal
/// I seen a lot diagrams on the web of several Valence/Arousal models, there are some slight variations, but you can see how you could
/// define your own emotional model for a video game character. Real people have emotions I will try to simulate them using a valence
/// /arousal model, there are two axis, valence and arousal, I can create a range then define different emotions as 2d points or vectors,
/// I added in a base emotional state that the character will start at as well as gravitate back towards over time. By attaching an 
/// emotion to an interaction with the character you can provoke an emotional response.
/// 
/// I will define the other two layers the same and 2d vectors, the hard part will be the interactions of all three layers.
/// 
/// Special thanks to Konstantin Samkovski, I don't think I would have gotten this far without our lengthy discussions on this topic.
/// </summary>

namespace EmotionalTest0
{
    class Program
    {

        static void Main(string[] args)
        {
            //define emotions
            Emotion blank = new Emotion("blank", "", 0, 0);
            Emotion anger = new Emotion("anger", "", -.66f, .66f);
            Emotion happiness = new Emotion("happiness", "", .66f, .66f);
            Emotion sadness = new Emotion("sadness", "", -.66f, -.66f);
            Emotion relaxed = new Emotion("relaxed", "", .66f, -.66f);
            Emotion rage = new Emotion("rage", "", -1, 1);
            Emotion bliss = new Emotion("bliss", "", 1, 1);
            Emotion dispair = new Emotion("dispair", "", -1, -1);
            Emotion comatose = new Emotion("comatose", "", 1, -1);
            //make a list of all emotions, then check to see what the character's currentState is closest to 
            List<Emotion> emotions = new List<Emotion>();
            emotions.Add(blank);
            emotions.Add(anger);
            emotions.Add(happiness);
            emotions.Add(sadness);
            emotions.Add(relaxed);
            emotions.Add(rage);
            emotions.Add(bliss);
            emotions.Add(dispair);
            emotions.Add(comatose);
            Personality test = new Personality(new Vector2(1, 1), new Vector2(0, 0), new Vector2(.25f, .25f));
            Personality test2 = new Personality(new Vector2(1, 1), new Vector2(0, 0), new Vector2(.5f, .02f));
            Personality test3 = new Personality(new Vector2(1, 1), new Vector2(0, 0), new Vector2(.02f, .5f));
            string userInput;
            bool quit = false;
            do
            {
                Console.Write("\nInput: ");
                userInput = Console.ReadLine();
                userInput = userInput.ToLower();
                // Split string on spaces to separate into words
                if (userInput.CompareTo("quit") == 0)//quit
                {
                    quit = true;
                }
                else if (userInput.CompareTo("") == 0)//no input
                {
                    Console.WriteLine(blank.GetName() + " " + blank.GetCopyOfPosition().GetX() + "/" + blank.GetCopyOfPosition().GetY());
                    test.Gravitate();
                    //2
                    test2.Gravitate();
                    //3
                    test3.Gravitate();
                }
                else
                {
                    foreach (Emotion e in emotions)
                    {
                        if (userInput.CompareTo(e.GetName()) == 0)
                        {
                            Console.WriteLine(e.GetName() + " " + e.GetCopyOfPosition().GetX() + "/" + e.GetCopyOfPosition().GetY());
                            test.ModifyCurrentState(e.GetCopyOfPosition());
                            //2
                            test2.ModifyCurrentState(e.GetCopyOfPosition());
                            //3
                            test3.ModifyCurrentState(e.GetCopyOfPosition());
                        }
                    }
                }
                Console.WriteLine("Test currentState: " + test.GetCurrentState().GetX() + "/" + test.GetCurrentState().GetY());
                float temp1, temp2 = 2;
                Emotion t_emotion = blank;
                foreach (Emotion e in emotions)
                {
                    temp1 = Vector2.Distance(test.GetCurrentState(), e.GetCopyOfPosition());
                    if (temp1 < temp2)
                    {
                        temp2 = temp1;
                        t_emotion = e;
                    }
                }
                Console.WriteLine("Character feels " + t_emotion.GetName());
                //2
                Console.WriteLine("Test2 currentState: " + test2.GetCurrentState().GetX() + "/" + test2.GetCurrentState().GetY());
                temp1 = 0;
                temp2 = 2;
                t_emotion = blank;
                foreach (Emotion e in emotions)
                {
                    temp1 = Vector2.Distance(test2.GetCurrentState(), e.GetCopyOfPosition());
                    if (temp1 < temp2)
                    {
                        temp2 = temp1;
                        t_emotion = e;
                    }
                }
                Console.WriteLine("Character2 feels " + t_emotion.GetName());
                //3
                Console.WriteLine("Test3 currentState: " + test3.GetCurrentState().GetX() + "/" + test3.GetCurrentState().GetY());
                temp1 = 0;
                temp2 = 2;
                t_emotion = blank;
                foreach (Emotion e in emotions)
                {
                    temp1 = Vector2.Distance(test3.GetCurrentState(), e.GetCopyOfPosition());
                    if (temp1 < temp2)
                    {
                        temp2 = temp1;
                        t_emotion = e;
                    }
                }
                Console.WriteLine("Character3 feels " + t_emotion.GetName());
            }
            while (!quit);
        }
    }
    public class Vector2
    {
        private float X;
        private float Y;

        public Vector2()
        {
            this.X = 0;
            this.Y = 0;
        }
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
        }
        public float GetX()
        {
            return X;
        }
        public float GetY()
        {
            return Y;
        }
        public void SetX(float x)
        {
            this.X = x;
        }
        public void SetY(float y)
        {
            this.Y = y;
        }
        public void Assign(Vector2 newVector)
        {
            this.X = newVector.GetX();
            this.Y = newVector.GetY();
        }
        /*public void Add(Vector2 vector)
        {
            this.X += vector.GetX();
            this.Y += vector.GetY();
        }
        public void Multiply(Vector2 vector)
        {
            this.X *= vector.GetX();
            this.Y *= vector.GetY();
        }*/
        public static Vector2 Add(Vector2 p_vector1, Vector2 p_vector2)
        {
            Vector2 r_vector = new Vector2(p_vector1.GetX() + p_vector2.GetX(), p_vector1.GetY() + p_vector2.GetY());
            return r_vector;
        }
        public static Vector2 Multiply(Vector2 p_vector1, Vector2 p_vector2)
        {
            Vector2 r_vector = new Vector2(p_vector1.GetX() * p_vector2.GetX(), p_vector1.GetY() * p_vector2.GetY());
            return r_vector;
        }
        public static bool Equals(Vector2 v1, Vector2 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y)
                return true;
            else
                return false;
        }
    }
    //character componant object
    /*public class Character
    {
        Personality personanlity;
        //Physical Description
        //Routine
        //Short term Memory?
        //Long term memory/knowledge

        Character(Personality p_personality)
        {
            personanlity = p_personality;
        }
    }*/
    public class Personality
    {
        //emotions based on valence and activation
        private Vector2 currentState = new Vector2(0, 0);//-1 to 1
        private Vector2 temperament = new Vector2(0, 0);//-1 to 1
        private Vector2 weight = new Vector2(0, 0);//.01 to 1

        //CONSTRUCTORS
        public Personality(Vector2 p_state, Vector2 p_temperament, Vector2 p_weight)
        {
            currentState.Assign(p_state);
            CheckCurrentState();
            temperament.Assign(p_temperament);
            CheckTemperament();
            weight.Assign(p_weight);
            CheckWeight();
        }
        //METHODS
        public void ModifyCurrentState(Vector2 p_emotion)//provoke
        {
            /*p_emotion.Multiply(weight);
            currentState.Add(p_emotion);
            CheckCurrentState();*/
            /*Vector2 temp = new Vector2(p_emotion.GetX(), p_emotion.GetY());
            temp.Multiply(weight);
            currentState.Add(temp);
            CheckCurrentState();*/
            Vector2 temp = Vector2.Multiply(p_emotion, weight);
            currentState = Vector2.Add(currentState, temp);
            CheckCurrentState();
        }
        public void Gravitate()
        {
            if (!currentState.Equals(temperament))
            {
                if (currentState.GetX() > temperament.GetX())
                {
                    currentState.Assign(new Vector2(currentState.GetX() - weight.GetX(), currentState.GetY()));
                }
                else if (currentState.GetX() < temperament.GetX())
                {
                    currentState.Assign(new Vector2(currentState.GetX() + weight.GetX(), currentState.GetY()));
                }
                if (currentState.GetY() > temperament.GetY())
                {
                    currentState.Assign(new Vector2(currentState.GetX(), currentState.GetY() - weight.GetY()));
                }
                else if (currentState.GetY() < temperament.GetY())
                {
                    currentState.Assign(new Vector2(currentState.GetX(), currentState.GetY() + weight.GetY()));
                }
            }
        }
        private void CheckCurrentState()
        {
            if (currentState.GetX() > 1)
                currentState.SetX(1);
            if (currentState.GetY() > 1)
                currentState.SetY(1);
            if (currentState.GetX() < -1)
                currentState.SetX(-1);
            if (currentState.GetY() < -1)
                currentState.SetY(-1);
        }
        private void CheckTemperament()
        {
            if (temperament.GetX() > 1)
                temperament.SetX(1);
            if (temperament.GetY() > 1)
                temperament.SetY(1);
            if (temperament.GetX() < -1)
                temperament.SetX(-1);
            if (temperament.GetY() < -1)
                temperament.SetY(-1);
        }
        private void CheckWeight()
        {
            if (weight.GetX() > 1)
                weight.SetX(1);
            if (weight.GetY() > 1)
                weight.SetY(1);
            if (weight.GetX() < .01f)
                weight.SetX(.01f);
            if (weight.GetY() < .01f)
                weight.SetY(.01f);
        }
        public void SetCurrentState(Vector2 newState)
        {
            currentState.Assign(newState);
        }
        public Vector2 GetCurrentState()
        {
            return currentState;
        }
    }

    class Emotion
    {
        private string name;
        private string description;
        private Vector2 position;

        public Emotion(string n, string d, float x, float y)
        {
            name = n;
            description = d;
            position = new Vector2(x, y);
            if (position.GetX() > 1)
                position.SetX(1);
            if (position.GetY() > 1)
                position.SetY(1);
            if (position.GetX() < -1)
                position.SetX(-1);
            if (position.GetY() < -1)
                position.SetY(-1);
        }
        public Emotion(string n, string d, Vector2 p_position)
        {
            name = n;
            description = d;
            position = p_position;
            if (position.GetX() > 1)
                position.SetX(1);
            if (position.GetY() > 1)
                position.SetY(1);
            if (position.GetX() < -1)
                position.SetX(-1);
            if (position.GetY() < -1)
                position.SetY(-1);
        }
        /*public Vector2 GetPosition()
        {
            return position;
        }*/
        public Vector2 GetCopyOfPosition()
        {
            Vector2 temp = new Vector2(position.GetX(), position.GetY());
            return temp;
        }
        public string GetName()
        {
            return name;
        }
        public string GetDescription()
        {
            return description;
        }
    }
}
