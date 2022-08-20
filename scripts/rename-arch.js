import path from "path";
import { execa } from "execa";
import { renameSync } from "fs";

async function main() {
  const rustInfo = (await execa("rustc", ["-vV"])).stdout;
  const targetTriple = /host: (\S+)/g.exec(rustInfo)[1];
  if (!targetTriple) {
    console.error("Failed to determine platform target triple");
  }

  let extension = ''
  if (process.platform === 'win32') {
    extension = '.exe'
  }

  renameSync(
    path.join(process.cwd(), "src-tauri", "bin", "eliteapi", `EliteAPI.Server${extension}`),
    path.join(process.cwd(), "src-tauri", "bin", "eliteapi", `eliteapi-${targetTriple}${extension}`)
  );
}

main().catch((e) => {
  throw e;
});
