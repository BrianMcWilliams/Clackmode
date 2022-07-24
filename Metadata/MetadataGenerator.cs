using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrackLayeringNFT
{
    public static class MetadataHelpers
    {
        public static bool IsImage(string filePath)
        {
            bool hasImageExtension = filePath.Contains(".png")
                || filePath.Contains(".jpg")
                || filePath.Contains("tif")
                || filePath.Contains("jpeg");

            return hasImageExtension;
        }
    }
    public class MetadataDescriptor
    {
        public MetadataDescriptor(string nameInit, string pathInit, int probabilityInit, Guid keyInit, Guid parentInit)
        {
            name = nameInit;
            probability = probabilityInit;
            key = keyInit;
            path = pathInit;
            parent = parentInit;

            isFolder = !MetadataHelpers.IsImage(path);
            MetadataDescriptor parentDesc;
            MetadataGenerator.GetMetadataForGuid(parentInit, out parentDesc);
            isCategory = isFolder && parentDesc.key == MetadataGenerator.m_MetadataDescriptors[0].key;
        }
        public MetadataDescriptor(string nameInit, string pathInit, int probabilityInit, Guid keyInit)
        {
            name = nameInit;
            probability = probabilityInit;
            key = keyInit;
            path = pathInit;

            isFolder = !MetadataHelpers.IsImage(path);
            isCategory = isFolder;
        }
        public int probability = -1;
        public Guid key;
        public string name = null;
        public string path = null;

        private string type = null;
        public bool isCategory;
        public bool isFolder;
        public bool isUniformProbability = false;
        public List<MetadataDescriptor> children = new List<MetadataDescriptor>();
        public List<Guid> exclusions = new List<Guid>();
        public Guid parent;
        public void AddMetadataChildItem(TreeViewItem item, int probability)
        {
            children.Add(new MetadataDescriptor((string)item.Header, (string)item.Tag, probability, Guid.Parse(item.Uid), key));
        }
        public string GetType()
        {
            if (type != null)
                return type;

            string childFolders = path.Remove(0, MetadataGenerator.m_MetadataDescriptors[0].path.Length);
            string[] childFoldersList = childFolders.Split('\\');

            type = childFoldersList[0];

            return childFoldersList[0];
        }
        public bool GetChoiceForCategory(out MetadataDescriptor result, ref System.Random rand, ref List<Guid> exclusionList)
        {
            result = null;

            Assert.IsTrue(isCategory);

            if (!exclusionList.Contains(key) && rand.Next(100) <= probability)
            {
                GetChildElementRecursive(out result, ref rand);
                return true;
            }
            else
                return false;
        }

        private void GetChildElementRecursive(out MetadataDescriptor result, ref Random rand)
        {
            if(children.Count > 0)
            {
                int probability = rand.Next(0, 100);

                foreach(MetadataDescriptor desc in children)
                {
                    if(desc.probability <= probability)
                    {
                        GetChildElementRecursive(out result, ref rand);
                        if (result != null)
                            return;       
                    }
                }

                result = children.Last();
                return;
            }

            result = null;
        }

        public bool HasChild(Guid guid, out MetadataDescriptor desc)
        {
            foreach (MetadataDescriptor child in children)
            {
                if (child.key == guid)
                {
                    desc = child;
                    return true;
                }
                else if (child.HasChild(guid, out desc))
                {
                    return true;
                }
            }

            desc = null;
            return false;
        }

        public bool HasChild(string name, out MetadataDescriptor desc)
        {
            foreach (MetadataDescriptor child in children)
            {
                if (child.name == name)
                {
                    desc = child;
                    return true;
                }
                else if (child.HasChild(name, out desc))
                {
                    return true;
                }
            }

            desc = null;
            return false;
        }

        public List<string> GetChildNames()
        {
            List<string> names = new List<string>();

            foreach(MetadataDescriptor child in children)
            {
                names.Add(child.name);
            }

            return names;
        }

    }
    public class Metadata
    {
        public Metadata()
        {
            attributes = new List<Attribute>();
        }
        public string name;
        public string description;
        public string image;
        public string track;
        public string mp4;
        public string date;
        public int index;

        public List<Attribute> attributes;
        public class Attribute
        {
            public Attribute(string type, string v)
            {
                trait_type = type;
                value = v;
            }
            public string trait_type;
            public string value;
        }
    }
    static class MetadataGenerator
    {
        public static List<TrackLayeringNFT.MetadataDescriptor> m_MetadataDescriptors = new List<TrackLayeringNFT.MetadataDescriptor>();
        public static int m_CollectionSize = 5000; // Make collection object that containts name description count, etc.
        public static void GetMetadataForGuid(string guid, out MetadataDescriptor outDescriptor)
        {
            GetMetadataForGuid(Guid.Parse(guid), out outDescriptor);
        }

        public static void GetMetadataForGuid(Guid guid, out MetadataDescriptor outDescriptor)
        {
            if (m_MetadataDescriptors[0].key == guid)
                outDescriptor = m_MetadataDescriptors[0];
            else
                m_MetadataDescriptors[0].HasChild(guid, out outDescriptor);
        }


        public static void GetMetadataForName(string name, out MetadataDescriptor outDescriptor)
        {
            if (m_MetadataDescriptors[0].name == name)
                outDescriptor = m_MetadataDescriptors[0];
            else
                m_MetadataDescriptors[0].HasChild(name, out outDescriptor);
        }
    }
}
