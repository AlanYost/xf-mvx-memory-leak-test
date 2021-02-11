# xf-mvx-memory-leak-test
Test XF Memory leak

# Notes: -

	After running from the IDE you need to then run the application without the debugger attached otherwise there are still memory leaks.

  Memory leaks are detected using MemoryTrackingService.  This seems to work well for us in identifying leaks and fixing them.  

NB. Running the same tests do NOT leak on Android….(Not upgraded to latest XF as no need)

# ListView Test:
	Tap on 'Show ListView Test' button with 10 Items configured —> 'ListView Test' page is shown, tap on any item a couple of times to reload the ListView collection (Simulating Expand/Collapse toggling, it reloads full collection or only even numbered items) —> Tap on ‘Back’ button to return to Main Page, then tap on ‘Show Memory Usage’ button to see results. ‘ListViewTestView’ and ‘ListViewTestViewModel’ do not leak memory. 
	
    Tap on 'Show ListView Test' button with 50 Items configured —> 'ListView Test' page is shown, tap on any item a couple of times to reload the ListView collection —> ‘ListViewTestView’ and ‘ListViewTestViewModel’ leak memory on iOS.


