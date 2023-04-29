async function DownloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);

    const url = URL.createObjectURL(blob);

    TriggerFileDownload(fileName, url);

    URL.revokeObjectURL(url);
}

function TriggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;

    if (fileName) {
        anchorElement.download = fileName;
    }

    anchorElement.click();
    anchorElement.remove();
}

function CopyTextToClipboard(text) {
    navigator.clipboard.writeText(text);
}

async function CopyRichTextToClipboard(elementId) {
    const content = document.getElementById(elementId);

    const clipboardItem = new ClipboardItem({
        "text/plain": new Blob(
            [content.innerText],
            { type: "text/plain" }
        ),
        "text/html": new Blob(
            [content.outerHTML],
            { type: "text/html" }
        ),
    });

    await navigator.clipboard.write([clipboardItem]);
}

async function CopyImageToClipboard(url) {
    const response = await fetch(url);
    const blob = await response.blob();

    await navigator.clipboard.write([
        new ClipboardItem({ "image/png": blob }),
    ]);
}