const editors = []

function findEditor(name) {
    const editor = editors.find(editor => editor.name === name);
    return editor;
}

export function editor(element, name, data) {
    const editor = monaco.editor.create(element, {
        value: data,
        language: 'json',
        automaticLayout: true,
        theme: 'vs-dark'
    });

    let existingEditor = findEditor(name);
    if (existingEditor) {
        existingEditor.editor = editor;
    } else {
        editors.push({ name: name, editor });
    }
}

export function updateEditor(element, name, data) {
    const result = findEditor(name)
    if (result != null && result.editor != null) {
        const model = result.editor.getModel();
        if (model != null) {
            model.setValue(data);
        }
    } else {
        console.error('editor is null', result);
    }
}

export function log(msg) {
    console.log(msg);
}