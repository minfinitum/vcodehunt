Simple windows tool to search text/code (and possibly binary) data. The intention was to provide simple text searching tool with context and history tracking.

Install:
Download vcodehunt.exe (grab a release\* version, it'll be faster).
Place it in your favourite tools folder. A configuration file is dropped in the local folder, put it somewhere in which it has write access.

Setup:
Run vcodehunt.exe
Check 'Options' and setup you're prefered editor. If you browse and select; notepad++, ultraedit, sublime_text and it will pick automatic configuration defaults.
Enable 'Show Context' set a resonable value (i.e. 5).
Enable Search Subfolders

Execution:
Type or browse to a code/text file path.
Setup you're inclusion file filters.
i.e..
	* - for all items
	*.c;*.cp;*.cpp;*.cc;*.cxx;*.c++;*.h;*.hp*.hpp;*.hxx; - c/cpp etc
	*ihavenoextension - files with no extensions

Type keywords and hit Enter/F5/Search
Magic happens...

M