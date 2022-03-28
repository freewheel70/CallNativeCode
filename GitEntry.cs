using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static CallNativeCode.GitUtility;

namespace CallNativeCode
{
    internal class GitEntry
    {
        public static void Start()
        {
            Console.WriteLine("Start our git journey~");
            string input;
            while ((input = Console.ReadLine()) != "q")
            {
                if (input is null || input == "")
                {
                    input = "C:\\Users\\xxx\\source\\repos\\docfx";
                    Console.WriteLine(input);
                }
                if (IsGitRepository(input))
                {
                    (string? url, string? branch, string? commit) = GetRepoInfo(input);
                    Console.WriteLine("Repo URL: " + url);
                    Console.WriteLine("Repo Branch: " + branch);
                    Console.WriteLine("Current Commit: " + commit);  
                }
            }

            Console.WriteLine("Bye!");
        }

        public static bool IsGitRepository(string path)
        {
            var gitPath = Path.Combine(path, ".git");
            if (Directory.Exists(gitPath) || File.Exists(gitPath))
            {
                if (git_repository_open(out _, gitPath) == 0)
                {
                    Console.WriteLine("It is a git repo: " + path);
                    return true;
                }
            }
            Console.WriteLine("It is NOT a git repo: " + path);

            return false;
        }

        public static unsafe (string? url, string? branch, string? commit) GetRepoInfo(string repoPath)
        {
            string? remoteName = null;
            var (url, branch, commit) = default((string, string, string));

            if (git_repository_open(out var pRepo, repoPath) != 0)
            {
                throw new ArgumentException($"Invalid git repo {repoPath}");
            }

            if (git_repository_head(out var pHead, pRepo) == 0)
            {
                commit = git_reference_target(pHead)->ToString();
                if (git_branch_name(out var pName, pHead) == 0)
                {
                    branch = Marshal.PtrToStringUTF8(pName);
                }

                remoteName = GetUpstreamRemoteName(pRepo, pHead);
                git_reference_free(pHead);
            }

            url = GetRepositoryUrl(pRepo, remoteName ?? "origin");

            git_repository_free(pRepo);

            return (url, branch, commit);

        }

        static unsafe string? GetUpstreamRemoteName(IntPtr pRepo, IntPtr pBranch)
        {
            string? result = null;
            if (git_branch_upstream(out var pUpstream, pBranch) == 0)
            {
                git_buf buffer;
                var pUpstreamName = git_reference_name(pUpstream);
                if (git_branch_remote_name(&buffer, pRepo, pUpstreamName) == 0)
                {
                    result = Marshal.PtrToStringUTF8(buffer.ptr) ?? "origin";
                    git_buf_free(&buffer);
                }
                git_reference_free(pUpstream);
            }
            return result;
        }

        static unsafe string? GetRepositoryUrl(IntPtr pRepo, string remoteName)
        {
            string? result = null;
            if (git_remote_lookup(out var pRemote, pRepo, remoteName) == 0)
            {
                result = Marshal.PtrToStringUTF8(git_remote_url(pRemote));
                git_remote_free(pRemote);
            }
            else
            {
                var remotes = default(git_strarray);
                if (git_remote_list(&remotes, pRepo) == 0)
                {
                    if (remotes.count > 0 && git_remote_lookup(out pRemote, pRepo, *remotes.strings) == 0)
                    {
                        result = Marshal.PtrToStringUTF8(git_remote_url(pRemote));
                        git_remote_free(pRemote);
                    }
                    git_strarray_free(&remotes);
                }
            }
            return result;
        }
    }
}
