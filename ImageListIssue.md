# Title
System.ArgumentOutOfRangeException upon "await Shell.Current.GoToAsync()"

# Description
An error triggers upon invoking `await Shell.Current.GoToAsync(nameof(ImageListPage), true, new Dictionary<string, object>` .
>System.ArgumentOutOfRangeException
>  Message=Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')

**Important note:**  _This code has been working for weeks and was working this morning when I first began some pre-release Android testing.  Then, suddenly, it began throwing this exception with no changes having been made to the code._

I initially assumed that something was weird with the data being supplied in `Images` but confirmed that it was still valid and correct.  During investigation, I determined the Exception was fired by the call to `GoToAsync()` in `GoToImages()` of `DetailVM.cs`.  I put a breakpoint on that line of code and, stepping through the code, it stopped on that line. 
 I then stepped forward.  It immediately produces the error and never even makes it to the destination `ImageListVM.cs` or `ImageListPage.cs`.

**Again**, _this was working fine for months and this happened only today during testing.  No changes were made to this code anytime recently and was working fine for a while and then just started throwing the Exception._

**Interesting note:**  When I debug the Windows version, it does **not** throw this Exception but, instead, falls into this code in `App.g.i.cs` and stops on the `System.Diagnostics.Debugger.Break()` within the if `DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION`:

```
    partial class App : global::Microsoft.Maui.MauiWinUIApplication
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        private bool _contentLoaded;
        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Platforms/Windows/App.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator);

#if DEBUG && !DISABLE_XAML_GENERATED_BINDING_DEBUG_OUTPUT
            DebugSettings.BindingFailed += (sender, args) =>
            {
                global::System.Diagnostics.Debug.WriteLine(args.Message);
            };
#endif
#if DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION
            UnhandledException += (sender, e) =>
            {
                if (global::System.Diagnostics.Debugger.IsAttached) global::System.Diagnostics.Debugger.Break();
            };
#endif
        }
    }
```

# Steps to Reproduce
1. Clone the repo, build for Android, and run a debug session.  It will come up with the Parks page showing an initial list.
2. Tap a tile; e.g., "Abraham Lincoln Birthplace".
3. Tap the button [Images].  The exception is immediately thrown.

# Did you find any workaround?
No.  I tried removing the QueryParameter info from the call and from the called VM.  The exception is still thrown.  I then tried creating a new page that uses the QueryParameter.  The exception is still thrown.

As noted, I confirmed that the content of the List<Image> is correct and valid -- so no weirdness in the data.

I've done clean and rebuilds.  I've done all the usual; i.e., closed Visual Studio, closed the Emulator, killed all VS-related processes, etc.  Didn't make any difference.

I even moved back to the last Stable release -- where this was definitely working -- and it still throws the Exception.

Since it successfully executes my code, I assume this is in the Maui or CommunityTookKit  libraries.

Yet, the other uses of `GoToAsync()` succeed with no Exception.  All of these actions use this method:
- Menu > tap any page from flyout.
- Menu > tap About, then tap [Tester].
- Menu > tap any page from flyout, then tap [Filter].

NOTE: The styles used in the ImageListPage are used by several other pages.


# Updates
I just tried this again -- with the QueryParameter code commented out -- and it didn't throw the Exception but displayed a blank ImageListPage.  So, I put the code back, and the Exception was thrown.  I commented the code again, and it still threw the Exception.

I went to a completely different system (my laptop) and cloned the repo.  Cleaned, build; debug session in light mode and the error did not appear.  Switched to dark mode and the error appeared.  Switched to light mode and the error continued to appear.

The next morning, did clean & build and ran with emulator in light mode.  It all worked find without an error.  I then closed the app, switched the emulator to dark mode, ran a debug session -- and the error appeared.  I then closed the app, switched the emulator to light mode, ran a debug session -- and the errror still appeared.

Removed the frame style from ImageListPage and did a test in light mode:  Error still appears.
Switched to API 33 emulator, clean & build; debug session in light mode: No error.
API 33 emulator; debug session in dark mode: Error appeared
API 33 emulator; debug session in light mode: Error continues to appear.
Removed the lable style.
API 31 emulator; debug session in light mode (Note: Emulator reset):  Error appeared.
Manually did a factory reset on the API 31 emulator, started the emulator from the Device Manager; debug session in light mode: Error still appears.
Restored the frame and lable styles to ImageListPage.
Closed the emulator, closed VS, restarted my computer.
Clean & rebuild, factory reset API 31 emulator; debug session in light mode: Error occurred.


