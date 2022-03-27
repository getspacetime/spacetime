import './main.css';
import * as monaco from 'monaco-editor';

window.something = () => {
  console.log('well hello');
}

window._editor = undefined;

window.editor = (element, data) => {
  console.log('initialize editor', element);
  const initialValue = `{
	"userId": 1,
	"id": 2,
	"title": "quis ut nam facilis et officia qui",
	"completed": false
}`;
  window._editor = monaco.editor.create(element, {
    value: initialValue,
    language: 'json',
    automaticLayout: true,
    theme: 'vs-dark'
  });
}

window.updateEditor = (element, data) => {
  const editor = window._editor;
  console.log('editor', editor);
  if (editor != null) {
    const model = editor.getModel();
    console.log('model', model);
    if (model != null) {
      model.setValue(data);
    }
  }
}