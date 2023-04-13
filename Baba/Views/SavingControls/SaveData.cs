using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

//
// Added to support serialization
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Engine.Views
{
    internal class SaveData
    {
        private bool saving = false;
        private bool loading = false;
        public GameState m_loadedState;
        public void saveSomething(GameState controls)
        {
            lock (this)
            {
                if (!saving)
                {
                    saving = true;
                    //
                    // Create something to save
                    while (loading)
                    {

                    }
                    finalizeSaveAsync(controls);
                }
            }
        }
        private async void finalizeSaveAsync(GameState controls)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("Controls", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(GameState));
                                mySerializer.Serialize(fs, controls);
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        // Ideally show something to the user, but this is demo code :)
                        Console.WriteLine(e);
                        Console.WriteLine("Unable to save data");


                    }
                }

                saving = false;
            });
        }

        public void loadSomething()
        {
            lock (this)
            {
                if (!loading)
                {
                    loading = true;
                    finalizeLoadAsync();
                    int x = 4;

                }
            }

        }
        public bool getIsLoading()
        {
            return loading;
        }
        private async void finalizeLoadAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        if (storage.FileExists("HighScores.xml"))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    XmlSerializer mySerializer = new XmlSerializer(typeof(GameState));
                                    m_loadedState = (GameState)mySerializer.Deserialize(fs);
                                    int x = 4;

                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                        Console.WriteLine("Unable to load save data");
                    }
                }

                loading = false;

            });

        }
    }

}
