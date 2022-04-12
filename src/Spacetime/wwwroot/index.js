import './main.css';
import * as monaco from 'monaco-editor';

window._editors = []

function findEditor(name) {
    return window._editors.find(editor => editor.name === name);
}

window.editor = (element, name, data) => {
    console.log('initialize editor', element);
    const editor = monaco.editor.create(element, {
        value: data,
        language: 'json',
        automaticLayout: true,
        theme: 'vs-dark'
    });

    window._editors.push({ name: name, editor });
}

window.updateEditor = (element, name, data) => {
    const editor = findEditor(name)
    console.log('editor', editor);
    if (editor != null) {
        const model = editor.editor.getModel();
        console.log('model', model);
        if (model != null) {
            model.setValue(data);
        }
    }
}