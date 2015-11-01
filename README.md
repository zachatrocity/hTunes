# hTunes
an itunes alternative



#TODO
~~1. A list box should list “All Music” and all playlists.  When All Music is selected, all the songs should be displayed in the data
grid.  When a specific playlist is selected, only songs from that playlist should be displayed in the data grid.~~

~~2. Songs should be displayed in a data grid control that shows each song’s title, artist, album, and genre.  ~~ 

~~3. When a song is selected, a detail pane should display details about the song including a picture of the album cover which
is obtained from last.fm.    ~~

~~4. A song can be played by selecting the song in the data grid pressing the Play button or by right‐clicking the song and
selecting “Play” from the context menu.  The Stop button should stop play.  Other buttons that pause, change the
volume, or advance to the next song would be helpful but are not required.  When a song is finished playing, ideally your
app should advance to the next song in the list, but this is not a requirement.~~

~~5. A toolbar should be used instead of a menu.  It should have a button for adding a song, adding a new playlist, and
showing the About dialog box.~~

~~6. The program should allow songs to be added to the listing of songs by launching an open dialog box and allowing the
user to select a song.  Provide a filter that by default shows .mp3, .m4a,.wma, and .wav files in the open file dialog box.  
After selecting a song, the program should read the metadata stored in the music file (if available), add the song to the
data grid, and select/highlight the song (so the user can see it among the list of potentially hundreds of songs).   ~~

7. The program should allow the user to create playlists in a manner similar to iTunes. The user should be able to create a
new playlist by selecting a New Playlist button from the toolbar.  Songs can be added to a playlist by dragging them from
the grid control and dropping them onto the desired playlist in the list box.  Each song that is dropped onto a playlist
should be added to the end of the playlist.  When viewing a playlist, the songs should be presented in order of position.   

~~8. Songs can be removed from the list of All Music by right‐clicking the song and selecting “Remove” from the context
menu.  A dialog box should confirm the removal.  A song can be removed from a playlist by viewing the songs in the
playlist, right‐clicking the song, and selecting “Remove from Playlist” from the context menu.  No dialog box is necessary.  
Note that the ordering of the songs in a playlist should be updated to reflect the removal of a song so if song at position 2
was removed, the 3rd song is now position 2, the 4th is position 3, and so on.~~

~~9. The list of all song files and playlists should be stored in a file called music.xml.  This file should reside in the same folder
as the app’s .exe.  The file should be loaded when the app is first executed and saved when the app terminates.  (Ideally
the file should be saved whenever a change is made to a song or playlist, but you should only save it once when the app
terminates.)~~

~~10. Song information can be modified directly in the data grid when viewing All Music.  Song info should not be editable
when viewing a playlist.~~

~~11. The main window should be resizable, and the contents should stretch/shrink to fit the size of the window.~~

~~12. There should be an About dialog box that identifies the programmers.~~

~~13. You must use the MusicLib library which will be supplied to you for separating the UI code from the business logic.~~

#Additional Requirements
Each additional requirement is worth 5 points:
1. Add the ability to rename and delete a playlist.  The user should be able to right‐click a playlist and select “Rename” from
the context menu.  Then a dialog box should display which allows the user to rename the playlist.  Make sure you only
change the playlist name if the user supplies a valid playlist (no blanks or identically named playlists).  A “Delete” option
should also be available from the context menu that deletes the playlist.

2. Use a Control Template to alter the look and behavior of the Play and Stop buttons.

3. Allow the user to enter a search string. As the string is being entered, the song list should be populated with any song
whose title, artist, album, or genre match any part of the search string.  An empty search string should yield all songs.  
