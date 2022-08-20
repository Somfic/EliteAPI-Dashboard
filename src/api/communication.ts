import readline from "readline";

export function onmessage(cb) {
  const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
    terminal: false,
  });

  rl.on("line", function (line) {
    cb(line);
  });
}
