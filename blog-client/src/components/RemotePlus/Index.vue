<template>
  <b-container class="post-container">
    <template v-if="getAllPosts.length > 0">
      <div v-for="(post, index) in getAllPosts" :key="index">
        <Post
          :id="post.Id.toString()"
          :title="post.Title"
          :content="post.Content"
          :createdAt="post.Created_at"
          :updatedAt="post.Updated_at"
          :image="post.Image"
          v-on:delete-post="deletePostInformation"
        />
      </div>
    </template>
    <template v-else>
      <h1>No hay posts registrados</h1>
    </template>
    <PostButton v-on:create-post="createPost" />
  </b-container>
</template>
<script>
import moment from "moment";
import blogType from "@/utils/blogType";
import { mapState, mapMutations } from "vuex";
import { getPosts, deletePost } from "@/api/post";
import Post from "@/components/Post/Index";
import PostButton from "@/components/Post/PostButton";
export default {
  name: "RemotePlus",
  data() {
    return {
      posts: [],
    };
  },
  components: {
    Post,
    PostButton,
  },
  computed: {
    ...mapState({
      blog: () => localStorage.blogType,
    }),
    getAllPosts() {
      return this.posts;
    },
    getCreatedAt(createdAt) {
      return moment(createdAt).calendar();
    },
  },
  async mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.REMOTE_PLUS);
    const posts = await getPosts();
    if (posts) {
      this.posts = posts.data;
    }
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
    createPost() {
      this.$router.push({ name: "NewRemotePost" });
    },
    deletePostInformation(id) {
      this.$swal
        .fire({
          title: "¿Estás seguro de eliminar el post?",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          cancelButtonText: "Cancelar",
          confirmButtonText: "Estoy seguro",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            try {
              const res = await deletePost(id);
              if (res) {
                this.$swal.fire(
                  "Eliminado",
                  "Tu post ha sido eliminado.",
                  "success"
                );
                const index = this.posts.findIndex((i) => i.id === id);
                this.posts.splice(index, 1);
              }
            } catch (error) {
              console.log(error);
              this.$swal.fire("Oops", "Tu post no fue eliminado.", "error");
            }
          }
        });
    },
  },
};
</script>
