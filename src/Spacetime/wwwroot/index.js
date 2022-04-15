import * as monaco from 'monaco-editor';
import './main.css';

/**
 * Add Monaco to the window
 * 
 * Long story short: I can't seem to get Monaco, or any npm package really,
 * to work with isolated scopes / loading scripts as modules. I beat my head
 * on this for several hours and found little documentation or any good examples
 * on other projects using npm packages and JS isolation with Blazor interop.
 * 
 * So, in our isolated module used in editor.js, we'll just grab this from the window scope.
 * 
 * I'd love to get this working in a cleaner way, but honestly I just want to move 
 * on with my life. If anyone smarter than me can figure this out, would love assistance.
 * 
 * Otherwise, this is the way we are going to live for now. 
 * 
 * Maybe I'll revisit this in a few months.
 * 
 * Maybe I won't.
 * 
 * Hours wasted: 4
 **/
window.monaco = monaco
