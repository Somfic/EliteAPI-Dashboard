#![cfg_attr(
    all(not(debug_assertions), target_os = "windows"),
    windows_subsystem = "windows"
)]

fn main() {
    let (mut rx, mut child) = Command::new_sidecar("eliteapi")
        .expect("failed to create `eliteapi` binary command")
        .spawn()
        .expect("Failed to spawn sidecar");

    tauri::async_runtime::spawn(async move {
        // read events such as stdout
        while let Some(event) = rx.recv().await {
            if let CommandEvent::Stdout(line) = event {
                window
                    .emit("message", Some(format!("'{}'", line)))
                    .expect("failed to emit event");
                
                    // write to stdin
                child.write("message from Rust\n".as_bytes()).unwrap();
            }
        }
    });

    tauri::Builder::default()
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
