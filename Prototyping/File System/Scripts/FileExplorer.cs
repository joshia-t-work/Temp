using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DKP.FileSystem
{
    public class FileExplorer : MonoBehaviour
	{
		public static FileExplorer Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
		private static void Exclusions()
		{
			FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
		}
		private static void QuickLink()
		{
			FileBrowser.AddQuickLink("Downloads", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads", null);
		}
		public static void LoadImage(Action<byte[]> action)
		{
			FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
			FileBrowser.SetDefaultFilter(".jpg");
			Exclusions();
			QuickLink();
			Instance.StartCoroutine(ShowLoadDialogCoroutine(action));
		}
		public static void LoadBook(Action<string> action)
		{
			FileBrowser.SetFilters(true, new FileBrowser.Filter("Book", ".dkpbook"));
			FileBrowser.SetDefaultFilter(".dkpbook");
			Exclusions();
			QuickLink();
			FileBrowser.ShowLoadDialog(
				(path) =>
				{
					action(path[0]);
				},
				() =>
				{

				},
				FileBrowser.PickMode.Files, false, null, null, "Load File", "Load"
			);
		}
		public static void SaveBook(Action<string> action)
		{
			FileBrowser.SetFilters(true, new FileBrowser.Filter("Book", ".dkpbook"));
			FileBrowser.SetDefaultFilter(".dkpbook");
			Exclusions();
			QuickLink();
			FileBrowser.ShowSaveDialog(
				(path) =>
				{
				action(path[0]);
				},
				() =>
				{

				},
				FileBrowser.PickMode.Files, false, null, null, "Untitled", "Save"
			);
		}
		private static IEnumerator ShowLoadDialogCoroutine(Action<byte[]> action)
		{
			// Show a load file dialog and wait for a response from user
			// Load file/folder: both, Allow multiple selection: true
			// Initial path: default (Documents), Initial filename: empty
			// Title: "Load File", Submit button text: "Load"
			yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load File", "Load");

			// Dialog is closed
			// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
			//Debug.Log(FileBrowser.Success);

			if (FileBrowser.Success)
			{
				// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
				//for (int i = 0; i < FileBrowser.Result.Length; i++)
				//	Debug.Log(FileBrowser.Result[i]);

				// Read the bytes of the first file via FileBrowserHelpers
				// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
				byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

				// Or, copy the first file to persistentDataPath
				//string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
				//FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
				action(bytes);
			} else
			{
				action(null);
			}
		}
		private static IEnumerator ShowSaveDialogCoroutine(Action<bool> action)
		{
			// Show a load file dialog and wait for a response from user
			// Load file/folder: both, Allow multiple selection: true
			// Initial path: default (Documents), Initial filename: empty
			// Title: "Load File", Submit button text: "Load"
			yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, null, "Untitled", "Save");

			// Dialog is closed
			// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
			//Debug.Log(FileBrowser.Success);

			if (FileBrowser.Success)
			{
				action(true);
			} else
			{
				action(false);
			}
		}
	}

}