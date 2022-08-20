import path from "path";
import { execa } from "execa";
import { renameSync } from "fs";

async function main() {
  const rustInfo = (await execa("rustc", ["-vV"])).stdout;
  const targetTriple = /host: (\S+)/g.exec(rustInfo)[1];
  if (!targetTriple) {
    console.error("Failed to determine platform target triple");
  }

  renameSync(
    path.join(process.cwd(), "src-tauri", "bin", "eliteapi"),
    path.join(process.cwd(), "src-tauri", "bin", `eliteapi-${targetTriple}`)
  );
}

main().catch((e) => {
  throw e;
});
