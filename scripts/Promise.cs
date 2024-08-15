using Godot;
using System;

public partial class Promise<R>
{
    ResolveCallback[] ResolveChain = Array.Empty<ResolveCallback>();
    RejectCallback[] RejectChain = Array.Empty<RejectCallback>();
    public delegate Promise<R> ResolveCallback(R data);
    public delegate Promise<R> RejectCallback(Exception err);
    public delegate void Initilizator(ResolveCallback Resolve, RejectCallback Reject);
    public Promise(Initilizator initilizator)
    {
        try
        {
            initilizator(Resolve, Reject);
        }
        catch (Exception e)
        {
            Reject(e);
        };
    }
    Promise<R> Resolve(R data)
    {
        if (ResolveChain.Length == 0) return this;
        ResolveCallback Resolve0 = ResolveChain[0];
        ResolveChain = Common.RemoveItemFromArray(ResolveChain, Resolve0);
        Promise<R> result = Resolve0(data);
        if (result == null) return this;
        result.ResolveChain = ResolveChain;
        result.RejectChain = RejectChain;
        return result;
    }
    Promise<R> Reject(Exception err)
    {
        if (RejectChain.Length == 0) return this;
        RejectCallback Reject0 = RejectChain[0];
        RejectChain = Common.RemoveItemFromArray(RejectChain, Reject0);
        Promise<R> result = Reject0(err);
        if (result == null) return this;
        result.ResolveChain = ResolveChain;
        result.RejectChain = RejectChain;
        result.Reject(err);
        return result;
    }
    public Promise<R> Then(ResolveCallback callback)
    {
        ResolveChain = Common.AppendItemToArray(ResolveChain, callback);
        return this;
    }
    public Promise<R> Catch(RejectCallback callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, callback);
        return this;
    }
}
